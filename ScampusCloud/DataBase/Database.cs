using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.DataBase
{
    public class Database
    {
        public string strConnectForLogin { get; set; }
        public Database()
        {
            strConnectForLogin = Convert.ToString(ConnectionString());
        }

        public SqlConnection Getconnection()
        {
            string strConnection = strConnectForLogin;

            SqlConnection sqlConnection = new SqlConnection(strConnection);
            return sqlConnection;
        }
        public SqlConnection GetconnectionForLogin()
        {
            SqlConnection sqlConnection = new SqlConnection(strConnectForLogin);
            return sqlConnection;
        }
        public string ConnectionString()
        {
            string ConnectionString = "";
            string filePath1 = Path.Combine(GetRootPath(), "Connection\\ConnectionString.txt");
            StreamReader sr = new StreamReader(filePath1);

            ConnectionString = sr.ReadToEnd();
            sr.Close();
            return ConnectionString;
        }
        public static string GetRootPath()
        {
            try
            {
                string completePath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath).Replace("/", "\\");
                string rootPath = completePath;// + "\\";
                return rootPath;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, ex.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}