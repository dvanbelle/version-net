using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Topping.Core.Data
{
    public class AccessorBase : IDisposable 
    {
        protected System.Data.SqlClient.SqlConnection _dbConn;

        public AccessorBase()
        {
            string str = ""; 
            // If Config Exists OVERWRITES
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ScrabbleConnectionString"] != null)
            {
                str = System.Configuration.ConfigurationManager.ConnectionStrings["ScrabbleConnectionString"].ConnectionString;
            }

            _dbConn = new SqlConnection();
            _dbConn.ConnectionString = str;
        }


        public void Dispose()
        {
            if (_dbConn != null)
            {
                if (_dbConn.State != ConnectionState.Closed)
                {
                    _dbConn.Close();
                }

                _dbConn.Dispose(); 
            }
        }

        protected bool SaveTable(DataTable tbl, SqlTransaction action)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * from " + tbl.TableName, _dbConn, action);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                SqlCommandBuilder build = new SqlCommandBuilder(adap);
                adap.Update(tbl);
                return true;
            }
            catch (Exception ee)
            {
                action.Rollback();
                throw(ee);
            }
        }

        protected bool SaveTable(DataRow[] rows, SqlTransaction action)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * from " + rows[0].Table.TableName, _dbConn, action);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                SqlCommandBuilder build = new SqlCommandBuilder(adap);
                adap.Update(rows);
                return true;
            }
            catch (Exception ee)
            {
                action.Rollback();
                Console.Write(ee.Message);
                throw (ee);
            }
        }

        public bool SaveTable(DataTable tbl)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT "  + GetFields(tbl.Columns) +  " from " + tbl.TableName, _dbConn);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.ContinueUpdateOnError = true; 
                SqlCommandBuilder build = new SqlCommandBuilder(adap);
                int x = adap.Update(tbl);
                return true;
            }
            catch (Exception ee)
            {
                throw (ee); 
            }
        }

        public string GetFields(DataColumnCollection coll)
        {
            string field = "";
            bool first = true;

            foreach (DataColumn col in coll)
            {
                if (!col.ColumnName.StartsWith("_"))
                {
                    if (first)
                    {
                        field = "[" + col.ColumnName + "]";
                        first = false;
                    }
                    else
                    {
                        field += ",[" + col.ColumnName + "]";
                    }
                }
            }

            return field; 
        }
    }
}
