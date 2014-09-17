using System.Web.Security;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace Topping.Web.Security
{

    public sealed class SQLiteMembershipProvider
    {



        private int newPasswordLength = 8;
        private string eventSource = "SQLiteMembershipProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please check the Event Log.";
        private string tableName = "users";
        private string connectionString;
        string pApplicationName = "TOPMACHINE"; 

        private const string encryptionKey = "AE09F72BA97CBBB5";

        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }


        //
        // System.Configuration.Provider.ProviderBase.Initialize Method
        //

        public  void Initialize()
        {

            ConnectionStringSettings ConnectionStringSettings =
              ConfigurationManager.ConnectionStrings["TopMachineDB"];

            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            connectionString = ConnectionStringSettings.ConnectionString;

        }


        public bool ValidateUser(string username, string password)
        {
            Initialize();
            bool isValid = false;

            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand("SELECT Password, IsApproved FROM `" + tableName + "`" +
                    " WHERE Username = $Username AND ApplicationName = $ApplicationName AND IsLockedOut = 0", conn);

            cmd.Parameters.Add("$Username", DbType.String, 255).Value = username;
            cmd.Parameters.Add("$ApplicationName", DbType.String, 255).Value = pApplicationName;

            SQLiteDataReader reader = null;
            bool isApproved = false;
            string pwd = "";

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();
                    pwd = reader.GetString(0);
                    int iApp = Convert.ToInt32(reader.GetValue(1));
                    if (iApp == 1) isApproved = true;
                }
                else
                {
                    return false;
                }

                reader.Close();

                if (CheckPassword(password, pwd))
                {
                    if (isApproved)
                    {
                        isValid = true;

                        SQLiteCommand updateCmd = new SQLiteCommand("UPDATE `" + tableName + "` SET LastLoginDate = $LastLoginDate" +
                                                                " WHERE Username = $Username AND ApplicationName = $ApplicationName", conn);

                        updateCmd.Parameters.Add("$LastLoginDate", DbType.DateTime).Value = DateTime.Now;
                        updateCmd.Parameters.Add("$Username", DbType.String, 255).Value = username;
                        updateCmd.Parameters.Add("$ApplicationName", DbType.String, 255).Value = pApplicationName;

                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    conn.Close();

                    UpdateFailureCount(username, "password");
                }
            }
            catch (SQLiteException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ValidateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return isValid;
        }


        //
        // UpdateFailureCount
        //   A helper method that performs the checks and updates associated with
        // password failure tracking.
        //

        private void UpdateFailureCount(string username, string failureType)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand("SELECT FailedPasswordAttemptCount, " +
                                              "  FailedPasswordAttemptWindowStart, " +
                                              "  FailedPasswordAnswerAttemptCount, " +
                                              "  FailedPasswordAnswerAttemptWindowStart " +
                                              "  FROM `" + tableName + "` " +
                                              "  WHERE Username = $Username AND ApplicationName = $ApplicationName", conn);

            cmd.Parameters.Add("$Username", DbType.String, 255).Value = username;
            cmd.Parameters.Add("$ApplicationName", DbType.String, 255).Value = pApplicationName;

            SQLiteDataReader reader = null;
            DateTime windowStart = new DateTime();
            int failureCount = 0;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    if (failureType == "password")
                    {
                        failureCount = reader.GetInt32(0);
                        try
                        {
                            windowStart = reader.GetDateTime(1);
                        }
                        catch
                        {
                            windowStart = DateTime.Now;
                        }
                    }

                    if (failureType == "passwordAnswer")
                    {
                        failureCount = reader.GetInt32(2);
                        windowStart = reader.GetDateTime(3);
                    }
                }

                reader.Close();

                DateTime windowEnd = windowStart.AddMinutes(5);

                if (failureCount == 0 || DateTime.Now > windowEnd)
                {
                    // First password failure or outside of PasswordAttemptWindow. 
                    // Start a new password failure count from 1 and a new window starting now.

                    if (failureType == "password")
                        cmd.CommandText = "UPDATE `" + tableName + "` " +
                                          "  SET FailedPasswordAttemptCount = $Count, " +
                                          "      FailedPasswordAttemptWindowStart = $WindowStart " +
                                          "  WHERE Username = $Username AND ApplicationName = $ApplicationName";

                    if (failureType == "passwordAnswer")
                        cmd.CommandText = "UPDATE `" + tableName + "` " +
                                          "  SET FailedPasswordAnswerAttemptCount = $Count, " +
                                          "      FailedPasswordAnswerAttemptWindowStart = $WindowStart " +
                                          "  WHERE Username = $Username AND ApplicationName = $ApplicationName";

                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("$Count", DbType.Int32).Value = 1;
                    cmd.Parameters.Add("$WindowStart", DbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("$Username", DbType.String, 255).Value = username;
                    cmd.Parameters.Add("$ApplicationName", DbType.String, 255).Value = pApplicationName;

                    if (cmd.ExecuteNonQuery() < 0)
                        throw new ProviderException("Unable to update failure count and window start.");
                }
                else
                {
                    if (failureCount++ >= 5)
                    {
                        // Password attempts have exceeded the failure threshold. Lock out
                        // the user.

                        cmd.CommandText = "UPDATE `" + tableName + "` " +
                                          "  SET IsLockedOut = $IsLockedOut, LastLockedOutDate = $LastLockedOutDate " +
                                          "  WHERE Username = $Username AND ApplicationName = $ApplicationName";

                        cmd.Parameters.Clear();

                        cmd.Parameters.Add("$IsLockedOut", DbType.Boolean).Value = true;
                        cmd.Parameters.Add("$LastLockedOutDate", DbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("$Username", DbType.String, 255).Value = username;
                        cmd.Parameters.Add("$ApplicationName", DbType.String, 255).Value = pApplicationName;

                        if (cmd.ExecuteNonQuery() < 0)
                            throw new ProviderException("Unable to lock out user.");
                    }
                    else
                    {
                        // Password attempts have not exceeded the failure threshold. Update
                        // the failure counts. Leave the window the same.

                        if (failureType == "password")
                            cmd.CommandText = "UPDATE `" + tableName + "` " +
                                              "  SET FailedPasswordAttemptCount = $Count" +
                                              "  WHERE Username = $Username AND ApplicationName = $ApplicationName";

                        if (failureType == "passwordAnswer")
                            cmd.CommandText = "UPDATE `" + tableName + "` " +
                                              "  SET FailedPasswordAnswerAttemptCount = $Count" +
                                              "  WHERE Username = $Username AND ApplicationName = $ApplicationName";

                        cmd.Parameters.Clear();

                        cmd.Parameters.Add("$Count", DbType.Int32).Value = failureCount;
                        cmd.Parameters.Add("$Username", DbType.String, 255).Value = username;
                        cmd.Parameters.Add("$ApplicationName", DbType.String, 255).Value = pApplicationName;

                        if (cmd.ExecuteNonQuery() < 0)
                            throw new ProviderException("Unable to update failure count.");
                    }
                }
            }
            catch (SQLiteException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UpdateFailureCount");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }
        }


        //
        // CheckPassword
        //   Compares password values based on the MembershipPasswordFormat.
        //

        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;


            pass1 = EncodePassword(password);

            if (pass1 == pass2)
            {
                return true;
            }

            return false;
        }


        //
        // EncodePassword
        //   Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.
        //

        private string EncodePassword(string password)
        {
            if (password == null) password = "";
            string encodedPassword = password;

            HMACSHA1 hash = new HMACSHA1();
            hash.Key = HexToByte(encryptionKey);
            encodedPassword =
              Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));


            return encodedPassword;
        }


        //
        // HexToByte
        //   Converts a hexadecimal string to a byte array. Used to convert encryption
        // key values from the configuration.
        //

        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }



        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }
    }
}