using ScampusCloud.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ScampusCloud.Utility
{
    public static class ErrorLogger
    {
        public static void WriteToErrorLog(string msg, string stkTrace, string innerExp, string title)
        {
            string errorPath = Path.Combine(Database.GetRootPath(), $@"Logs\\Errors");

            if (string.IsNullOrEmpty(errorPath))
            {
                return;
            }

            if (!(Directory.Exists(errorPath + $@"Logs\\Errors")))
            {
                Directory.CreateDirectory(errorPath + $@"Logs\\Errors");
            }


            try
            {
                string errorFilePath;

                var directory = new DirectoryInfo(errorPath + $@"Logs\\Errors");

                if (directory.GetFiles().Any())
                {
                    var myFile = (from f in directory.GetFiles()
                                  orderby f.LastWriteTime descending
                                  select f).First();

                    if (myFile == null)
                    {
                        errorFilePath = errorPath + $@"Logs\\Errors" + "errlog_" + DateTime.Now.Ticks.ToString() + ".txt";
                    }
                    else
                    {
                        errorFilePath = myFile.FullName;
                    }
                }
                else
                {
                    errorFilePath = errorPath + $@"Logs\\Errors" + "errlog_" + DateTime.Now.Ticks.ToString() + ".txt";
                }

                // the file is already exist, now check the size and if it is higher than defined size than create new one so that it can be
                // send via email.
                if (File.Exists(errorFilePath))
                {
                    FileInfo objFileInfo = new FileInfo(errorFilePath);
                    long fileSize = objFileInfo.Length;

                    if (fileSize >= 1024 * 1024)
                    {
                        errorFilePath = errorPath + $@"Logs\\Errors" + "errlog_" + DateTime.Now.Ticks.ToString() + ".txt";
                        //File.Create(errorFilePath);

                    }
                }

                //common.StrErrorFilePath = errorFilePath;
                StringBuilder strBld = new StringBuilder();
                using (StreamWriter strWriter = new StreamWriter(errorFilePath, true))
                {
                    strBld.Append("Title: " + title + "\r\n");
                    strBld.Append("Message: " + msg + "\r\n");
                    strBld.Append("StackTrace: " + stkTrace + "\r\n");
                    if (innerExp != string.Empty)
                        strBld.Append("Inner Exception: " + innerExp + "\r\n");
                    // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                    strBld.Append("Date/Time: " + DateTime.Now.ToString() + "\r\n");
                    strBld.Append("===========================================================================================\r\n");

                    strWriter.Write(strBld.ToString());
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void WriteToErrorLog(Exception ex, string title)
        {
            string msg = ex.Message;
            string stkTrace = ex.StackTrace.ToString();
            string innerExp = string.Empty;
            if (ex.InnerException != null)
            {
                innerExp = ex.InnerException.ToString();
            }

            //string errorPath = Startup.contantRootPath;
            string errorPath = Path.Combine(Database.GetRootPath(), $@"Logs\\Errors");
            if (string.IsNullOrEmpty(errorPath))
            {
                return;
            }

            if (!(Directory.Exists(errorPath + $@"Logs\\Errors")))
            {
                Directory.CreateDirectory(errorPath + $@"Logs\\Errors");
            }


            try
            {
                string errorFilePath;

                var directory = new DirectoryInfo(errorPath + $@"Logs\\Errors");

                if (directory.GetFiles().Any())
                {
                    var myFile = (from f in directory.GetFiles()
                                  orderby f.LastWriteTime descending
                                  select f).First();

                    if (myFile == null)
                    {
                        errorFilePath = errorPath + $@"Logs\\Errors" + "errlog_" + DateTime.Now.Ticks.ToString() + ".txt";
                    }
                    else
                    {
                        errorFilePath = myFile.FullName;
                    }
                }
                else
                {
                    errorFilePath = errorPath + $@"Logs\\Errors" + "errlog_" + DateTime.Now.Ticks.ToString() + ".txt";
                }

                // the file is already exist, now check the size and if it is higher than defined size than create new one so that it can be
                // send via email.
                if (File.Exists(errorFilePath))
                {
                    FileInfo objFileInfo = new FileInfo(errorFilePath);
                    long fileSize = objFileInfo.Length;

                    if (fileSize >= 1024 * 1024)
                    {
                        errorFilePath = errorPath + $@"Logs\\Errors" + "errlog_" + DateTime.Now.Ticks.ToString() + ".txt";
                        //File.Create(errorFilePath);

                    }
                }

                //common.StrErrorFilePath = errorFilePath;

                using (StreamWriter strWriter = new StreamWriter(errorFilePath, true))
                {
                    StringBuilder strBld = new StringBuilder();

                    strBld.Append("Title: " + title + "\r\n");
                    strBld.Append("Message: " + msg + "\r\n");
                    strBld.Append("StackTrace: " + stkTrace + "\r\n");
                    if (innerExp != string.Empty)
                        strBld.Append("Inner Exception: " + innerExp + "\r\n");
                    // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                    strBld.Append("Date/Time: " + DateTime.Now.ToString() + "\r\n");
                    strBld.Append("===========================================================================================\r\n");

                    strWriter.Write(strBld.ToString());
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void WriteLogForQuickbookIntegration(string msg, string stkTrace, string innerExp, string title)
        {
            string errorPath = Path.Combine(Database.GetRootPath(), $@"Logs\\Errors");

            if (!(Directory.Exists(errorPath + $@"Logs\\Errors")))
            {
                Directory.CreateDirectory(errorPath + $@"Logs\\Errors");
            }


            try
            {
                string errorFilePath;

                var directory = new DirectoryInfo(errorPath + $@"Logs\\Errors");

                if (directory.GetFiles().Any())
                {
                    var myFile = (from f in directory.GetFiles()
                                  orderby f.LastWriteTime descending
                                  select f).First();

                    if (myFile == null)
                    {
                        errorFilePath = errorPath + $@"Logs\\Errors" + "log_" + DateTime.Now.Ticks.ToString() + ".txt";
                    }
                    else
                    {
                        errorFilePath = myFile.FullName;
                    }
                }
                else
                {
                    errorFilePath = errorPath + $@"Logs\\Errors" + "log_" + DateTime.Now.Ticks.ToString() + ".txt";
                }

                // the file is already exist, now check the size and if it is higher than defined size than create new one so that it can be
                // send via email.
                if (File.Exists(errorFilePath))
                {
                    FileInfo objFileInfo = new FileInfo(errorFilePath);
                    long fileSize = objFileInfo.Length;

                    if (fileSize >= 1024 * 1024)
                    {
                        errorFilePath = errorPath + $@"Logs\\Errors" + "log_" + DateTime.Now.Ticks.ToString() + ".txt";
                        //File.Create(errorFilePath);

                    }
                }

                //common.StrErrorFilePath = errorFilePath;

                using (StreamWriter strWriter = new StreamWriter(errorFilePath, true))
                {
                    StringBuilder strBld = new StringBuilder();

                    strBld.Append("Title: " + title + "\r\n");
                    strBld.Append("Message: " + msg + "\r\n");
                    strBld.Append("StackTrace: " + stkTrace + "\r\n");
                    if (innerExp != string.Empty)
                        strBld.Append("Inner Exception: " + innerExp + "\r\n");
                    // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                    strBld.Append("Date/Time: " + DateTime.Now.ToString() + "\r\n");
                    strBld.Append("===========================================================================================\r\n");

                    strWriter.Write(strBld.ToString());
                }
            }
            catch
            {
                // ignored
            }
        }

    }
}