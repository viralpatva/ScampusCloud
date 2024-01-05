using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.DataBase
{
    public class DBExtension
    {
        public DBExtension()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public void ForEach<T>(IEnumerable<T> enumeration, Action<T> action)
        {

            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// Convert  list to Data Table
        /// </summary>
        /// <typeparam name="T">Target Class</typeparam>
        /// <param name="varlist">list you want to convert it to Data Table</param>
        /// <param name="fn">Delegate Function to Create Row</param>
        /// <returns>Data Table That Represent List data</returns>
        public DataTable ToADOTable<T>(IEnumerable<T> varlist, CreateRowDelegate<T> fn)
        {
            DataTable toReturn = new DataTable();

            // Could add a check to verify that there is an element 0
            T TopRec = varlist.ElementAtOrDefault(0);

            if (TopRec == null)
                return toReturn;

            // Use reflection to get property names, to create table
            // column names

            PropertyInfo[] oProps = ((Type)TopRec.GetType()).GetProperties();

            foreach (PropertyInfo pi in oProps)
            {
                Type pt = pi.PropertyType;
                if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    pt = Nullable.GetUnderlyingType(pt);
                toReturn.Columns.Add(pi.Name, pt);
            }

            foreach (T rec in varlist)
            {
                DataRow dr = toReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    object o = pi.GetValue(rec, null);
                    if (o == null)
                        dr[pi.Name] = DBNull.Value;
                    else
                        dr[pi.Name] = o;
                }
                toReturn.Rows.Add(dr);
            }

            return toReturn;
        }

        /// <summary>
        /// Convert  list to Data Table
        /// </summary>
        /// <typeparam name="T">Target Class</typeparam>
        /// <param name="varlist">list you want to convert it to Data Table</param>
        /// <returns>Data Table That Represent List data</returns>
        public DataTable ToADOTable<T>(IEnumerable<T> varlist)
        {
            DataTable toReturn = new DataTable();

            // Could add a check to verify that there is an element 0
            T TopRec = varlist.ElementAtOrDefault(0);

            if (TopRec == null)
                return toReturn;

            // Use reflection to get property names, to create table
            // column names

            PropertyInfo[] oProps = ((Type)TopRec.GetType()).GetProperties();

            foreach (PropertyInfo pi in oProps)
            {
                Type pt = pi.PropertyType;
                if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    pt = Nullable.GetUnderlyingType(pt);
                toReturn.Columns.Add(pi.Name, pt);
            }

            foreach (T rec in varlist)
            {
                DataRow dr = toReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    object o = pi.GetValue(rec, null);

                    if (o == null)
                        dr[pi.Name] = DBNull.Value;
                    else
                        dr[pi.Name] = o;
                }
                toReturn.Rows.Add(dr);
            }

            return toReturn;
        }

        /// <summary>
        /// Convert Data Table To List of Type T
        /// </summary>
        /// <typeparam name="T">Target Class to convert data table to List of T </typeparam>
        /// <param name="datatable">Data Table you want to convert it</param>
        /// <returns>List of Target Class</returns>
        public List<T> ConvertToList<T>(DataTable dt) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in dt.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                Temp = dt.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;

                //Type tClass = typeof(T);
                //PropertyInfo[] pClass = tClass.GetProperties();
                //List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
                //T cn;
                //foreach (DataRow item in dt.Rows)
                //{
                //    cn = (T)Activator.CreateInstance(tClass);
                //    foreach (PropertyInfo pc in pClass)
                //    {
                //        // Can comment try catch block. 
                //        try
                //        {
                //            DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                //            if (d != null)
                //                pc.SetValue(cn, item[pc.Name], null);
                //        }
                //        catch
                //        {
                //        }
                //    }
                //    Temp.Add(cn);
                //}
                //return Temp;
            }
            catch { return Temp; }
        }

        /// <summary>
        /// Converts DataRows in T type list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        public List<T> ConvertTo<T>(IList<DataRow> rows)
        {
            List<T> list = null;
            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Convert DataRow into T Object
        /// </summary>   
        public T CreateItem<T>(DataRow row)
        {
            string columnName;
            T obj = default(T);
            if (row != null)
            {
                //Create the instance of type T
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    columnName = column.ColumnName;
                    //Get property with same columnName
                    PropertyInfo prop = obj.GetType().GetProperty(columnName, (BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase));
                    if (prop != null)
                    {
                        try
                        {
                            //Get value for the column
                            object value = (row[columnName].GetType() == typeof(DBNull)) ? null : row[columnName];
                            //Set property value
                            prop.SetValue(obj, value, null);
                        }
                        catch
                        {
                            throw;
                            //Catch whatever here
                        }
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// Converts DatATable into List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<T> ToList<T>(DataTable table)
        {
            if (table == null)
                return null;
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                rows.Add(row);
            return ConvertTo<T>(rows);
        }

        /// <summary>
        /// Convert Data Table To List of Type T
        /// </summary>
        /// <typeparam name="T">Target Class to convert data table to List of T </typeparam>
        /// <param name="datatable">Data Table you want to convert it</param>
        /// <returns>List of Target Class</returns>
        public T ToEntity<T>(DataTable datatable) where T : new()
        {
            return ToList<T>(datatable).FirstOrDefault();
        }

        public T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties; Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch { return obj; }
        }

        public delegate object[] CreateRowDelegate<T>(T t);


    }
}