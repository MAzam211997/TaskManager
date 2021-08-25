using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;


namespace TaskManager.Services.GeneralServices
{
    public partial class ClientBaseServices
    {
        private readonly string _clientConnectionString;

        //-----------------------------------------------------------------------------------------------------

        private readonly bool _isOwner = false;

        private SqlTransaction _txn;

        public IDbTransaction DBTransaction
        {
            get => _txn;
            set => _txn = (SqlTransaction)value;
        }

        public ClientBaseServices(string clientConnectionString)
            : this(clientConnectionString, null)
        { }

        public ClientBaseServices(string clientConnectionString, IDbTransaction txn)
        {
            if (txn?.Connection == null)
            {
                _isOwner = true;
            }
            else
            {
                _txn = (SqlTransaction)txn;
                _isOwner = false;
            }

            _clientConnectionString = clientConnectionString;
        }

        public static object NullCheckString(string self)
        {
            return string.IsNullOrEmpty(self == "null" ? null : self) ? (object)DBNull.Value : self;
        }
        public static object NullCheckDateString(string self)
        {
            return string.IsNullOrEmpty(self) ? "00000000" : self;
        }

        public static object NullCheckString(bool? self)
        {
            return self ?? (object)DBNull.Value;
        }

        public static object NullCheckString(DateTime? self)
        {
            return self == null || self == DateTime.MinValue ? (object)DBNull.Value : self;
        }

        public static object NullCheckString(int? self)
        {
            return self == null || self == int.MinValue ? (object)DBNull.Value : self;
        }

        public static object NullCheckString(decimal? self)
        {
            return self == null || self == decimal.MinValue ? (object)DBNull.Value : self;
        }

        public static IDbTransaction BeginTransaction(string clientConnectionString)
        {
            SqlConnection cnx = new SqlConnection(clientConnectionString);
            cnx.Open();
            return cnx.BeginTransaction();
        }

        #region Launcher DB Executions

        protected object ExecuteScalarCommand(string procName, params IDataParameter[] procParams)
        {
            SqlConnection cnx = null;
            object obj = null;
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(procName) { CommandType = CommandType.StoredProcedure, CommandTimeout = 0 };
                if (procParams != null)
                    for (int index = 0; index < procParams.Length; index++)
                        cmd.Parameters.Add(procParams[index]);

                da.SelectCommand = (SqlCommand)cmd;

                if (_isOwner)
                {
                    cnx = new SqlConnection(_clientConnectionString);
                    cmd.Connection = cnx;
                    cnx.Open();
                }
                else
                {
                    cmd.Connection = _txn.Connection;
                    cmd.Transaction = _txn;
                }

                obj = cmd.ExecuteScalar();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                da.Dispose();

                cmd?.Dispose();

                if (_isOwner)
                {
                    cnx?.Dispose();
                }
            }
            return obj;
        }

        protected DataSet ExecuteQueryCommand(string procName, params IDataParameter[] procParams)
        {
            SqlConnection cnx = null;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(procName) { CommandType = CommandType.StoredProcedure, CommandTimeout = 0 };
                if (procParams != null)
                    for (int index = 0; index < procParams.Length; index++)
                        cmd.Parameters.Add(procParams[index]);

                da.SelectCommand = (SqlCommand)cmd;

                if (_isOwner)
                {
                    cnx = new SqlConnection(_clientConnectionString);
                    cmd.Connection = cnx;
                    cnx.Open();
                }
                else
                {
                    cmd.Connection = _txn.Connection;
                    cmd.Transaction = _txn;
                }

                //Fill the dataset
                da.Fill(ds);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                da.Dispose();

                cmd?.Dispose();

                if (_isOwner)
                {
                    cnx?.Dispose();
                }
            }
            return ds;
        }

        protected int ExecuteNonQueryCommand(string procName, params IDataParameter[] procParams)
        {
            SqlConnection cnx = null;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(procName) { CommandType = CommandType.StoredProcedure, CommandTimeout = 0 };
                for (int index = 0; index < procParams.Length; index++)
                    cmd.Parameters.Add(procParams[index]);

                if (_isOwner)
                {
                    cnx = new SqlConnection(_clientConnectionString);
                    cmd.Connection = cnx;
                    cnx.Open();
                }
                else
                {
                    cmd.Connection = _txn.Connection;
                    cmd.Transaction = _txn;
                }

                return cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                cmd?.Dispose();

                if (_isOwner)
                {
                    cnx?.Dispose();
                }
            }
        }

        protected DataTable ExecuteSqlReader(string procName, params IDataParameter[] procParams)
        {
            SqlConnection sqlConnection = null;

            DataTable dataTable = new DataTable();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(procName, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 0;

                    if (procParams != null)
                        for (int index = 0; index < procParams.Length; index++)
                            sqlCommand.Parameters.Add(procParams[index]);

                    if (_isOwner)
                    {
                        sqlConnection = new SqlConnection(_clientConnectionString);
                        sqlCommand.Connection = sqlConnection;
                        sqlConnection.Open();
                    }
                    else
                    {
                        sqlCommand.Connection = _txn.Connection;
                        sqlCommand.Transaction = _txn;
                    }

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        // IDataReader Load function
                        dataTable.Load(reader);
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {

                if (_isOwner)
                {
                    sqlConnection?.Dispose();
                }
            }

            return dataTable;
        }

        protected void ExecuteAsyncNonQueryCommand(string procName, params IDataParameter[] procParams)
        {
            SqlConnection cnx = null;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(procName) { CommandType = CommandType.StoredProcedure };

                for (int index = 0; index < procParams.Length; index++)
                    cmd.Parameters.Add(procParams[index]);

                if (_isOwner)
                {
                    string cnnStr = _clientConnectionString + "; Async=true";
                    cnx = new SqlConnection(cnnStr);
                    cmd.CommandTimeout = 400;
                    cmd.Connection = cnx;
                    cnx.Open();
                }
                else
                {
                    cmd.Connection = _txn.Connection;
                    cmd.Transaction = _txn;
                }

                //cmd.ExecuteNonQuery();
                cmd.BeginExecuteNonQuery(new AsyncCallback(AsyncCommandCompletionCallback), cmd);

            }
            catch (Exception exc)
            {
                throw exc;
            }
            //finally
            //{
            //    if (_isOwner)
            //        cnx.Dispose();

            //    if (cmd != null)
            //        cmd.Dispose();
            //}
        }


        //public static void ExecuteAsyncNonQueryCommand(string procedureName, params IDataParameter[] parameters)
        //{
        //    //string cnnStr = _clientConnectionString + "; Async=true";
        //    string cnnStr = _clientConnectionString + "; Asynchronous Processing=true";
        //    using (SqlConnection connection = new SqlConnection(cnnStr))
        //    {
        //        SqlCommand command = connection.CreateCommand();
        //        command.CommandText = procedureName;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandTimeout = 400;
        //        if (parameters != null)
        //            for (int index = 0; index < parameters.Length; index++)
        //                command.Parameters.Add(parameters[index]);

        //        connection.Open();
        //        command.BeginExecuteNonQuery(new AsyncCallback(AsyncCommandCompletionCallback), command);
        //    }
        //}

        // --------------------------------------------------------------------------------

        //public static void ExecuteNonQueryAsyc(string procedureName, IList<SqlParameter> parameters)
        //{
        //    using (SqlConnection connection = new SqlConnection(_clientConnectionString + ";Async=true;"))
        //    {
        //        SqlCommand command = connection.CreateCommand();
        //        command.CommandText = procedureName;
        //        command.CommandType = CommandType.StoredProcedure;
        //        if (parameters != null)
        //            for (int index = 0; index < parameters.Count; index++)
        //                command.Parameters.Add(parameters[index]);
        //        connection.Open();
        //        command.BeginExecuteNonQuery(new AsyncCallback(AsyncCommandCompletionCallback), command);
        //    }
        //}

        static void AsyncCommandCompletionCallback(IAsyncResult result)
        {
            SqlCommand cmd = null;
            try
            {        // Get our command object from AsyncState, then call EndExecuteNonQuery.        
                cmd = (SqlCommand)result.AsyncState;
                cmd.EndExecuteNonQuery(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd?.Connection.Close();
                cmd?.Dispose();
            }
        }

        //--------------------------------------------------------------------------------


        protected DataSet ExecuteQueryAdhocSQL(string sql)
        {
            SqlConnection cnx = null;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql) { CommandType = CommandType.Text, CommandTimeout = 0 };
                da.SelectCommand = (SqlCommand)cmd;

                if (_isOwner)
                {
                    cnx = new SqlConnection(_clientConnectionString);
                    cmd.Connection = cnx;
                    cnx.Open();
                }
                else
                {
                    cmd.Connection = _txn.Connection;
                    cmd.Transaction = _txn;
                }

                //Fill the dataset
                da.Fill(ds);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                da.Dispose();

                cmd?.Dispose();

                if (_isOwner)
                {
                    cnx?.Dispose();
                }
            }
            return ds;
        }

        protected int ExecuteNonQueryAdhocSQL(string sql)
        {
            SqlConnection cnx = null;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql) { CommandType = CommandType.Text, CommandTimeout = 0 };
                if (_isOwner)
                {
                    cnx = new SqlConnection(_clientConnectionString);
                    cmd.Connection = cnx;
                    cnx.Open();
                }
                else
                {
                    cmd.Connection = _txn.Connection;
                    cmd.Transaction = _txn;
                }

                return cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc.InnerException;
            }
            finally
            {
                if (_isOwner)
                {
                    cnx?.Dispose();
                }

                cmd?.Dispose();
            }
        }

        #endregion

        #region Create Parameters

        protected static SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue)
        {
            SqlParameter param = new SqlParameter(paramName, paramType);
            param.Value = paramValue;
            return param;
        }

        protected static SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, int size)
        {
            SqlParameter param = CreateParameter(paramName, paramType, paramValue);
            param.Size = size;
            return param;
        }

        protected static SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, ParameterDirection direction)
        {
            SqlParameter param = CreateParameter(paramName, paramType, paramValue);
            param.Direction = direction;
            return param;
        }

        protected static SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, int size, ParameterDirection direction)
        {
            SqlParameter param = CreateParameter(paramName, paramType, paramValue, size);
            param.Direction = direction;
            return param;
        }

        #endregion

        #region Business Stuff

        protected static Int16 GetInt16(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToInt16(row[columnName]) : NullInt16;
        }

        protected static int GetInt(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToInt32(row[columnName]) : NullInt;
        }

        protected static long GetInt64(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToInt64(row[columnName]) : NullInt;
        }

        protected static DateTime GetDateTime(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToDateTime(row[columnName]) : NullDateTime;
        }
        protected static decimal GetDecimal(DataRow row, string columnName, bool zeroIfNull = false)
        {
            return row[columnName] != DBNull.Value ? Convert.ToDecimal(row[columnName]) : zeroIfNull ? 0 : 0;
        }
        protected static bool GetBool(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value && Convert.ToBoolean(row[columnName]);
        }
        protected static string GetString(DataRow row, string columnName)
        {
            return (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value) ? Convert.ToString(row[columnName]) : NullString;
        }
        protected static Byte[] GetImage(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? (Byte[])row[columnName] : NullBytes;
        }
        protected static double GetDouble(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToDouble(row[columnName]) : NullDouble;
        }
        protected static float GetFloat(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToSingle(row[columnName]) : NullFloat;
        }
        protected static Guid GetGuid(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? (Guid)row[columnName] : NullGuid;
        }
        protected static long GetLong(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? (long)row[columnName] : NullLong;
        }
        protected static short GetShort(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToInt16(row[columnName]) : NullShort;
        }
        protected static string ToneString(DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
                return NullString;
            else
            {
                StringBuilder s = new StringBuilder(Convert.ToString(row[columnName]));
                s.Replace("'", "''");
                return s.ToString();
            }
        }
        protected static string ToneString(string cvalue)
        {
            if (string.IsNullOrEmpty(cvalue))
                return string.Empty;
            else
            {
                StringBuilder s = new StringBuilder(cvalue);
                s.Replace("'", "''");
                return s.ToString();
            }
        }


        public static DateTime NullDateTime = DateTime.MinValue;
        public static decimal NullDecimal = decimal.MinValue;
        public static double NullDouble = double.MinValue;
        public static Guid NullGuid = Guid.Empty;
        public static Int16 NullInt16 = Int16.MinValue;
        public static int NullInt = int.MinValue;
        public static long NullLong = long.MinValue;
        public static short NullShort = short.MinValue;
        public static float NullFloat = float.MinValue;
        public static string NullString = string.Empty;
        public static Byte[] NullBytes = new Byte[] { };
        public static DateTime SqlMaxDate = new DateTime(9999, 1, 3, 23, 59, 59);
        public static DateTime SqlMinDate = new DateTime(1753, 1, 1, 00, 00, 00);
        #endregion

        public static DataTable ParseString(string languageItems)
        {
            DataTable dt = new DataTable();
            DataColumn dcCultureCode = new DataColumn("CultureCode");
            DataColumn dcName = new DataColumn("NameInCulture");
            dt.Columns.Add(dcCultureCode);
            dt.Columns.Add(dcName);


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

    }

}
