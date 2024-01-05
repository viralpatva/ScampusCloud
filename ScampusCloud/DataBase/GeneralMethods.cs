using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ScampusCloud.DataBase
{
    public class GeneralMethods
    {
        #region SqlQuery

        //SqlDataAdapter da;
        SqlConnection conn;
        //DataTable dt;

        public DataSet ExecuteSelectQur(string strQuery)
        {
            Database odatabse = new Database();
            conn = odatabse.Getconnection();
            DataSet ds = new DataSet();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var objAdp = new SqlDataAdapter(strQuery, conn))
                    {
                        objAdp.Fill(ds);
                    }
                }
                return ds;

            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public void ExecuteQur(string strQuery)
        {
            Database odatabase = new Database();
            conn = odatabase.Getconnection();
            SqlCommand objCmd = new SqlCommand();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    objCmd = new SqlCommand(strQuery, conn);
                    objCmd.ExecuteNonQuery();
                }

            }
            catch (Exception oException)
            {

                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public void ExecuteScalarQur(string strQuery)
        {
            Database odatabse = new Database();
            //int i;
            SqlCommand objCmd = new SqlCommand();
            conn = odatabse.Getconnection();
            //SqlTransaction objTran;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            //objTran = conn.BeginTransaction();
            try
            {
                lock (conn)
                {
                    objCmd = new SqlCommand(strQuery, conn);
                    //objCmd.Transaction = objTran;
                    objCmd.ExecuteScalar();
                    // objTran.Commit();
                }
            }
            catch (Exception oException)
            {
                throw oException;

            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public string ExecuteScalarQurForId(string strQuery)
        {
            Database odatabse = new Database();
            conn = odatabse.Getconnection();

            SqlCommand objCmd = new SqlCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            try
            {
                lock (conn)
                {
                    objCmd = new SqlCommand(strQuery, conn);
                    return Convert.ToString(objCmd.ExecuteScalar());
                }
            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public object ExecuteScalarQuery(string strQuery)
        {
            Database odatabse = new Database();
            object objRetValue = null;
            SqlCommand objCmd = new SqlCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            try
            {
                lock (conn)
                {
                    objCmd = new SqlCommand(strQuery, conn);
                    objRetValue = objCmd.ExecuteScalar();
                }
            }
            catch (Exception oException)
            {

                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

            return objRetValue;
        }

        public T ExecuteObject<T>(string strQuery) where T : new()
        {

            Database odatabse = new Database();
            conn = odatabse.Getconnection();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var da = new SqlDataAdapter(strQuery, conn))
                    {
                        //da = new SqlDataAdapter(strQuery, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DBExtension _DBExtension = new DBExtension();
                        return _DBExtension.ToEntity<T>(dt);
                    }
                }
            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public DateTime GetDate(string strQuery)
        {
            Database odatabse = new Database();
            //int i;

            conn = odatabse.Getconnection();
            //OleDbTransaction objTran;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var da = new SqlDataAdapter(strQuery, conn))
                    {
                        //da = new SqlDataAdapter(strQuery, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return Convert.ToDateTime(Convert.ToString(dt.Rows[0][0]));
                    }
                }
            }
            catch (Exception oException)
            {

                throw oException;
                // objTran.Rollback();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public List<T> GetList<T>(string strQuery) where T : new()
        {
            Database odatabse = new Database();
            conn = odatabse.Getconnection();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var da = new SqlDataAdapter(strQuery, conn))
                    {
                        //da = new SqlDataAdapter(strQuery, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DBExtension _DBExtension = new DBExtension();
                        return _DBExtension.ToList<T>(dt);
                    }
                }


            }
            catch (Exception oException)
            {

                throw oException;
                // objTran.Rollback();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }


        public List<T> GetOnlineList<T>(string strQuery, SqlConnection NewConnection) where T : new()
        {
            //Database odatabse = new Database();
            //int i;
            Database odatabse = new Database();
            conn = odatabse.GetconnectionForLogin();
            //conn = NewConnection;
            //OleDbTransaction objTran;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var da = new SqlDataAdapter(strQuery, conn))
                    {
                        //da = new SqlDataAdapter(strQuery, conn);
                        //objCmd.Transaction = objTran;
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DBExtension _DBExtension = new DBExtension();
                        return _DBExtension.ToList<T>(dt);
                    }
                }
            }
            catch (Exception oException)
            {

                throw oException;
                // objTran.Rollback();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }
        #endregion

        #region Online reporting

        public DataTable ExecuteSelectQurForOnlineReporting(string strQuery, SqlConnection cn)
        {
            Database odatabse = new Database();
            conn = cn;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var objAdp = new SqlDataAdapter(strQuery, conn))
                    {
                        //SqlDataAdapter objAdp = new SqlDataAdapter(strQuery, conn);
                        objAdp.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public bool ExecuteAny(string strQuery, SqlConnection cn)
        {
            Database odatabse = new Database();
            conn = odatabse.GetconnectionForLogin();
            DataTable dt = new DataTable();
            try
            {
                bool flag = false;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var objAdp = new SqlDataAdapter(strQuery, conn))
                    {
                        //SqlDataAdapter objAdp = new SqlDataAdapter(strQuery, conn);
                        objAdp.Fill(dt);
                    }
                }
                if (dt != null && dt.Rows.Count > 0 && Convert.ToInt16(dt.Rows[0][0]) > 0)
                    flag = true;
                return flag;
            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public T ExecuteObjectForOnlineReporting<T>(string strQuery) where T : new()
        {
            //conn = cn;
            Database odatabse = new Database();
            conn = odatabse.GetconnectionForLogin();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var da = new SqlDataAdapter(strQuery, conn))
                    {
                        //da = new SqlDataAdapter(strQuery, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DBExtension _DBExtension = new DBExtension();
                        return _DBExtension.ToEntity<T>(dt);
                    }
                }

            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public void ExecuteQurForOnlineReporting(string strQuery)
        {
            Database odatabse = new Database();
            conn = odatabse.GetconnectionForLogin();
            SqlCommand objCmd = new SqlCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            try
            {
                lock (conn)
                {
                    objCmd = new SqlCommand(strQuery, conn);
                    objCmd.ExecuteNonQuery();
                }
            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public string ExecuteScalarQurForIdForOnlineReporting(string strQuery, SqlConnection cn)
        {
            Database odatabse = new Database();
            conn = odatabse.GetconnectionForLogin();
            //int i;
            SqlCommand objCmd = new SqlCommand();
            conn = cn;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            try
            {
                lock (conn)
                {
                    objCmd = new SqlCommand(strQuery, conn);
                    return Convert.ToString(objCmd.ExecuteScalar());
                }
            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public string ExcecuteUsingSp(QueryBuilder objQueryBuilder)
        {
            try
            {
                Database odatabse = new Database();
                conn = odatabse.Getconnection();
                using (var cmd = new SqlCommand(objQueryBuilder.StoredProcedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in objQueryBuilder.FieldValueCollection)
                    {
                        cmd.Parameters.AddWithValue(item.ColumnName, item.ColumnValue);
                    }
                    conn.Open();
                    return Convert.ToString(cmd.ExecuteScalar());
                }
            }
            catch (Exception oException)
            {
                string strexception = oException.ToString();

                throw;
            }
            finally
            {
            }

        }

        public DataTable ExcecuteDataTableSp(QueryBuilder objQueryBuilder)
        {
            Database odatabse = new Database();
            conn = odatabse.Getconnection();
            DataTable dt = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var cmd = new SqlCommand(objQueryBuilder.StoredProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var item in objQueryBuilder.FieldValueCollection)
                        {
                            cmd.Parameters.AddWithValue(item.ColumnName, item.ColumnValue);
                        }
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                    }
                }
                return dt;
            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }
        public List<T> GetListForOnlineReporting<T>(string strQuery) where T : new()
        {
            Database odatabse = new Database();
            conn = odatabse.GetconnectionForLogin();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                lock (conn)
                {
                    using (var da = new SqlDataAdapter(strQuery, conn))
                    {
                        //da = new SqlDataAdapter(strQuery, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DBExtension _DBExtension = new DBExtension();
                        return _DBExtension.ToList<T>(dt);
                    }
                }


            }
            catch (Exception oException)
            {

                throw oException;
                // objTran.Rollback();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        #endregion
    }
}