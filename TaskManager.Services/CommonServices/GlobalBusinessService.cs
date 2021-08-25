using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

using TaskManager.Services.GeneralServices;

namespace TaskManager.Services.CommonServices
{
    public class GlobalBusinessService : ClientBaseServices
    {
        private string ProcStarter => "_";

        public GlobalBusinessService(string clientConnectionString)
            : base(clientConnectionString)
        {

        }

        /// <summary>
        /// Used in SCN
        /// </summary>
        /// <param name="table"></param>
        /// <param name="key"></param>
        /// <param name="isExact"></param>
        /// <param name="subSupport"></param>
        /// <returns></returns>
        public DataTable AutoCompleteCodeNameSearch(string table, int userID = 0, string key = "", bool isExact = false, bool subSupport = false)
        {
            DataSet ds = ExecuteQueryCommand(ProcStarter + "GLB_CodeNameSearch",
                                             CreateParameter("Table", SqlDbType.VarChar, table),
                                             CreateParameter("SearchKey", SqlDbType.NVarChar, key),
                                             CreateParameter("IsExact", SqlDbType.Bit, isExact),
                                             CreateParameter("UserID", SqlDbType.Int, userID),
                                             CreateParameter("SubSupport", SqlDbType.Bit, subSupport));

            DataTable dt = null;

            if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                dt = ds.Tables[0];

            return dt;
        }

        public DataTable SelectAllCurrencies()
        {
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Currency_SelectAll");

            DataTable dt = null;
            if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                dt = ds.Tables[0];

            return dt;
        }

        public bool IsValueDuplicate(string table, string column, string value, string groupColumn = "", string groupValue = "", string PrimaryColumn = "", string PrimaryColumnValue = "")
        {
            DataRow dr = null;

            DataSet ds = ExecuteQueryCommand(ProcStarter + "GLB_IsValueDuplicate",
                                             CreateParameter("Table", SqlDbType.VarChar, table),
                                             CreateParameter("Column", SqlDbType.VarChar, column),
                                             CreateParameter("Value", SqlDbType.VarChar, NullCheckString(string.IsNullOrEmpty(value) ? string.Empty : value.Trim())),
                                             CreateParameter("GroupColumn", SqlDbType.VarChar, NullCheckString(groupColumn)),
                                             CreateParameter("GroupValue", SqlDbType.VarChar, NullCheckString(string.IsNullOrEmpty(groupValue) ? string.Empty : groupValue.Trim())),
                                             CreateParameter("PrimaryColumn", SqlDbType.VarChar, NullCheckString(PrimaryColumn)),
                                             CreateParameter("PrimaryColumnValue", SqlDbType.VarChar, NullCheckString(string.IsNullOrEmpty(PrimaryColumnValue) ? string.Empty : PrimaryColumnValue.Trim())));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        dr = ds.Tables[0].Rows[0];
            }

            int res = (int)dr[0];
            return (res != 0);
        }

    }

}
