using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ScampusCloud.DataBase
{
    public class Database
    {
        public string strConnectForLogin { get; set; }
        public Database()
        {
            //strConnectForLogin = Convert.ToString(Startup.ConnectionString);
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
    }
}