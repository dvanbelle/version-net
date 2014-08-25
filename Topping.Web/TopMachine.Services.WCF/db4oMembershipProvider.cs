/// Copyright (c) 2008-2011 Brad Williams
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
/// associated documentation files (the "Software"), to deal in the Software without restriction,
/// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
/// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
/// subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all copies or substantial
/// portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
/// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
/// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
/// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
/// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using db4oProviders;
using db4oProviders.Common;
using System.Configuration;

namespace Topping.Web.Security.db4o
{
    public sealed class db4oMembershipProviderWcf 
    {
        private static readonly string PROVIDER_NAME = "db4oMembershipProvider";
        private readonly IConnectionStringStore ConnectionStringStore;
        private readonly int newPasswordLength = 5;
        private readonly IValidationKeyInfo ValidationKeyInfo;
        private string connectionString;

        public db4oMembershipProviderWcf()
            : this(new ConfigurationManagerConnectionStringStore(), new ValidationKeyInfo("system.web/machineKey"))
        {
        }

        public db4oMembershipProviderWcf(IConnectionStringStore connectionStringStore, IValidationKeyInfo validationKeyInfo)
        {
            ConnectionStringStore = connectionStringStore;
            ValidationKeyInfo = validationKeyInfo;
        }
        private NameValueCollection ConvertProviderToNameValue(MembershipProvider pb) 
        {
            NameValueCollection config = new NameValueCollection();

            config.Add("connectionStringName", "connectionstringname");
            config.Add("ApplicationName", pb.ApplicationName);
            config.Add("EnablePasswordReset", pb.EnablePasswordReset.ToString());
            config.Add("EnablePasswordRetrieval", pb.EnablePasswordRetrieval.ToString());
            config.Add("MaxInvalidPasswordAttempts", pb.MaxInvalidPasswordAttempts.ToString());
            config.Add("MinRequiredNonAlphanumericCharacters", pb.MinRequiredNonAlphanumericCharacters.ToString());
            config.Add("MinRequiredPasswordLength", pb.MinRequiredPasswordLength.ToString());
            config.Add("PasswordAttemptWindow", pb.PasswordAttemptWindow.ToString());
            config.Add("PasswordFormat", pb.PasswordFormat.ToString());
            config.Add("PasswordStrengthRegularExpression", pb.PasswordStrengthRegularExpression);
            config.Add("RequiresQuestionAndAnswer", pb.RequiresQuestionAndAnswer.ToString());
            config.Add("RequiresUniqueEmail", pb.RequiresUniqueEmail.ToString());
            return config;
        
        }
        public void Initialize()
        {
            if (!string.IsNullOrEmpty(connectionString)) 
            {
                return;
            }
           
            //config = ConvertProviderToNameValue(Membership.Provider);   

            NameValueCollection config = new NameValueCollection();
            config.Add("connectionStringName", "connectionstringname");
            config.Add("ApplicationName", "TOPMACHINE");
            config.Add("EnablePasswordReset", "true");
            config.Add("EnablePasswordRetrieval", "false");
            config.Add("MaxInvalidPasswordAttempts", "3");
            config.Add("MinRequiredNonAlphanumericCharacters", "0");
            config.Add("MinRequiredPasswordLength", "5");
            config.Add("PasswordAttemptWindow", "5");
            config.Add("PasswordFormat", "Hashed");
            config.Add("PasswordStrengthRegularExpression", "");
            config.Add("RequiresQuestionAndAnswer", "false");
            config.Add("RequiresUniqueEmail", "true");

            ConnectionStringSettings ConnectionStringSettings =
            ConfigurationManager.ConnectionStrings["Db4o"];

            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }
            connectionString = ConnectionStringSettings.ConnectionString;
            Initialize("", config);
        

          
            //applicationName = "TOPMACHINE";
        }

        public void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(name))
                name = PROVIDER_NAME;

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "db4o ASP.NET Membership Provider");
            }
                      

            //applicationName = Utils.DefaultIfBlank(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
            //if (connectionString == null)
            //{
            //    connectionString = ConnectionStringStore.GetConnectionString(config["connectionStringName"]);
            //    if (connectionString == null)
            //        throw new ProviderException("Connection string cannot be blank.");
            //}
            //maxInvalidPasswordAttempts = Convert.ToInt32(Utils.DefaultIfBlank(config["maxInvalidPasswordAttempts"], "5"));
            //passwordAttemptWindow = Convert.ToInt32(Utils.DefaultIfBlank(config["passwordAttemptWindow"], "3"));
            //minRequiredNonAlphanumericCharacters = Convert.ToInt32(Utils.DefaultIfBlank(config["minRequiredNonAlphanumericCharacters"], "0"));
            //minRequiredPasswordLength = Convert.ToInt32(Utils.DefaultIfBlank(config["minRequiredPasswordLength"], "4"));
            //passwordStrengthRegularExpression = Convert.ToString(Utils.DefaultIfBlank(config["passwordStrengthRegularExpression"], ""));
            //enablePasswordReset = Convert.ToBoolean(Utils.DefaultIfBlank(config["enablePasswordReset"], "true"));
            //enablePasswordRetrieval = Convert.ToBoolean(Utils.DefaultIfBlank(config["enablePasswordRetrieval"], "true"));
            //requiresQuestionAndAnswer = Convert.ToBoolean(Utils.DefaultIfBlank(config["requiresQuestionAndAnswer"], "false"));
            //requiresUniqueEmail = Convert.ToBoolean(Utils.DefaultIfBlank(config["requiresUniqueEmail"], "true"));

            string tempPasswordFormat = config["passwordFormat"] ?? "Hashed";

            switch (tempPasswordFormat)
            {
                case "Hashed":
                    passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }
           // var cfg = ConfigurationManager.GetSection("system.web/machineKey");
            // il ne lit pas le bon fichier de config (il lit le fichier config par defaut du framework)
            if (ValidationKeyInfo.IsAutoGenerated() && (PasswordFormat != MembershipPasswordFormat.Clear))
                throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
        }

        public  void UpdateUser(MembershipUser user)
        {
            UpdateUser(u => u.Username == user.UserName && u.ApplicationName == ApplicationName,
                       updating =>
                           {
                               updating.Email = user.Email;
                               updating.Comment = user.Comment;
                               updating.IsApproved = user.IsApproved;
                           });
        }

        public User UpdateUser(string username, Action<User> userUpdate)
        {
            return UpdateUser(u => u.Username == username && u.ApplicationName == ApplicationName, userUpdate);
        }

        private User UpdateUser(Predicate<User> userPredicate, Action<User> userUpdate)
        {
            SafeDB dbc = new SafeDB(connectionString);
            
                IList<User> results = dbc.Query(userPredicate);

                if (results.Count != 1)
                    return null;

                User found = results[0];

                if (userUpdate != null)
                    userUpdate(found);

                found.LastActivityDate = DateTime.Now;

                dbc.Store(found);
                dbc.Close(); 
                return found;
           
        }

        public  bool ValidateUser(string username, string password)
        {
            bool isValid = false;
            Initialize(); 
            User user = UpdateUser(username,
                                   updating =>
                                       {
                                           if (CheckPassword(password, updating.Password))
                                           {
                                               if (updating.IsApproved)
                                               {
                                                   isValid = true;
                                                   updating.LastLoginDate = DateTime.Now;
                                               }
                                           }
                                           else
                                           {
                                              // updating.UpdateFailureCount("password", this);
                                           }
                                       }
                );

            if (user == null)
                return false;

            return isValid;
        }

        private bool CheckPassword(string password, string dbPassword)
        {
            string pass1 = password;
            string pass2 = dbPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbPassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
                default:
                    break;
            }

            if (pass1 == pass2)
                return true;

            return false;
        }

        private string EncodePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "";

            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1 {Key = HexToByte(ValidationKeyInfo.GetKey())};
                    encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;
        }

        private string UnEncodePassword(string encodedPassword)
        {
            if (string.IsNullOrEmpty(encodedPassword))
                return "";

            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        private static byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length/2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i*2, 2), 16);
            return returnBytes;
        }


        #region properties

        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private int maxInvalidPasswordAttempts;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private string passwordStrengthRegularExpression;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;

        public string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public bool EnablePasswordReset
        {
            get { return enablePasswordReset; }
        }

        public bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }

        public  bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        public bool RequiresUniqueEmail
        {
            get { return requiresUniqueEmail; }
        }

        public  int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        public  int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        public  MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        public  int MinRequiredNonAlphanumericCharacters
        {
            get { return minRequiredNonAlphanumericCharacters; }
        }

        public  int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        public string PasswordStrengthRegularExpression
        {
            get { return passwordStrengthRegularExpression; }
        }

        #endregion
    }
}