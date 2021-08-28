using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace TaskManager.Entities.GeneralModels
{
    public class GeneralHelper
    {
        public static string HashSHA1(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            byte[] buffer = Encoding.Default.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            byte[] output = cryptoTransformSHA1.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in output)
                sb.Append(b.ToString("x2").ToUpper());
            return sb.ToString();
        }

        public static bool isEmailValid(string email, bool applyStrict)
        {
            string patternLenient = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            string patternStrict = @"^(([^<>()[\]\.,;:\s@""]+"
            + @"(\.[^<>()[\]\.,;:\s@""]+)*)|("".+""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
            + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
            + @"[a-zA-Z]{2,}))$";
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(applyStrict ? patternStrict : patternLenient);
            bool isValid = regEx.IsMatch(email);
            return isValid;
        }

        #region  Create Dynamic Table 
        public static DataTable TablePropertySet(Type p, string[] skipProperties = null)
        {
            DataTable dataTable = new DataTable();
            foreach (PropertyInfo pInfo in p.GetProperties())
            {
                if (skipProperties != null && skipProperties.Length > 0)
                {
                    if (!skipProperties.Contains(pInfo.Name))
                    {
                        string propertyName = pInfo.Name;
                        dataTable.Columns.Add(propertyName);
                    }
                }
                else
                {
                    string propertyName = pInfo.Name;
                    dataTable.Columns.Add(propertyName);
                }

            }
            return dataTable;
        }
        public static DataTable TablePropertySetValues(object obj, Type p, DataTable dataTable)
        {
            DataRow row = dataTable.NewRow();
            if (obj != null)
            {

                foreach (PropertyInfo pInfo in p.GetProperties())
                {
                    if (dataTable.Columns.Contains(pInfo.Name))
                    {
                        string propertyName = pInfo.Name;
                        var value = obj.GetType().GetProperty(propertyName).GetValue(obj, null);
                        row[propertyName] = value;
                    }

                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
        #endregion

        #region object To DataTable Conversion
        public static DataTable ParseString(string languageItems)
        {
            DataTable dt = new DataTable();
            DataColumn dcCultureCode = new DataColumn("CultureCode");
            DataColumn dcName = new DataColumn("NameInCulture");
            dt.Columns.Add(dcCultureCode);
            dt.Columns.Add(dcName);

            if (languageItems == null || languageItems == "")
                return dt;

            var langItems = languageItems.Split(new[] { "<-|->" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
            foreach (string langItem in langItems)
            {
                var nameInCultures = langItem.Split(new[] { "-<|>-" }, StringSplitOptions.None).Select(s => s.Trim()).ToList();
                DataRow dr = dt.NewRow();
                dr["CultureCode"] = nameInCultures.FirstOrDefault();
                dr["NameInCulture"] = nameInCultures.LastOrDefault();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region Convert DataTable to List
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName] != DBNull.Value ? dr[column.ColumnName] : string.Empty, null);
                }
            }
            return obj;
        }

        #endregion
    }


    public static class EnumHelper<T>
    {
        public static string GetEnumDescription(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }

    // Create our own utility for exceptions
    public sealed class ExceptionUtility
    {
        // All methods are static, so this can be private
        private ExceptionUtility()
        { }

        // Log an Exception
        public static void LogException(Exception exc, string source)
        {
            //// Include enterprise logic for logging exceptions
            //// Get the absolute path to the log file
            //string logFile = "App_Data/ErrorLog.txt";
            //logFile = HttpContext.Current.Server.MapPath(logFile);

            //// Open the log file for append and write the log
            //StreamWriter sw = new StreamWriter(logFile, true);
            //sw.WriteLine("********** {0} **********", DateTime.Now);
            //if (exc.InnerException != null)
            //{
            //    sw.Write("Inner Exception Type: ");
            //    sw.WriteLine(exc.InnerException.GetType().ToString());
            //    sw.Write("Inner Exception: ");
            //    sw.WriteLine(exc.InnerException.Message);
            //    sw.Write("Inner Source: ");
            //    sw.WriteLine(exc.InnerException.Source);
            //    if (exc.InnerException.StackTrace != null)
            //    {
            //        sw.WriteLine("Inner Stack Trace: ");
            //        sw.WriteLine(exc.InnerException.StackTrace);
            //    }
            //}
            //sw.Write("Exception Type: ");
            //sw.WriteLine(exc.GetType().ToString());
            //sw.WriteLine("Exception: " + exc.Message);
            //sw.WriteLine("Source: " + source);
            //sw.WriteLine("Stack Trace: ");
            //if (exc.StackTrace != null)
            //{
            //    sw.WriteLine(exc.StackTrace);
            //    sw.WriteLine();
            //}
            //sw.Close();
        }

        // Notify System Operators about an exception
        public static void NotifySystemOps(Exception exc)
        {
            // Include code for notifying IT system operators
        }
    }


}