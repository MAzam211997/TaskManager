using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskManager.Entities;
using TaskManager.Entities.GeneralModels;
using TaskManager.Services.GeneralServices;

namespace TaskManager.Services.CommonServices
{
    public class UserBusinessService : ClientBaseServices
    {
        public UserBusinessService(string clientConnectionstring)
            : base(clientConnectionstring)
        {

        }
        private string ProcStarter => "_";

        /// <summary>
        /// Used in user profile
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Users SelectUserByUserID(int userID)
        {
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Users_SelectByUserID",
                                             CreateParameter("UserID", SqlDbType.Int, userID));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return CreateUserFromDataRow(ds.Tables[0].Rows[0]);

            return null;
        }

        public Users SelectUserForGotPasswordByUserName(string userName)
        {
            DataRow dr = null;
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Users_SelectByUserName",
                                             CreateParameter("UserName", SqlDbType.VarChar, userName));

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        dr = ds.Tables[0].Rows[0];
            }
            return CreateUserFromDataRow(dr);


        }

        public int InsertUser(Users user)
        {
            if (user == null) return 0;

            SqlParameter newIDParameter = CreateParameter("NewUserID", SqlDbType.Int, -1, ParameterDirection.Output);

            ExecuteNonQueryCommand(ProcStarter + "CFN_Users_Insert",
                CreateParameter("RoleID", SqlDbType.Int, user.RoleID),
                CreateParameter("Initial", SqlDbType.NVarChar, user.Initials),
                CreateParameter("FullName", SqlDbType.NVarChar, user.FullName),
                CreateParameter("EmailAddress", SqlDbType.NVarChar, user.EmailAddress),
                CreateParameter("Mobile", SqlDbType.VarChar, user.Mobile),
                CreateParameter("Password", SqlDbType.VarChar, user.Password),
                CreateParameter("IsActive", SqlDbType.Bit, user.IsActive),
                CreateParameter("CreatedBy", SqlDbType.Int, user.CreatedBy),
                CreateParameter("CreatedDate", SqlDbType.DateTime2, user.CreatedDate),
                CreateParameter("ArabicName", SqlDbType.VarChar, user.ArabicName),
                newIDParameter);

            return (int)newIDParameter.Value;
        }

        public bool UpdateUser(Users user)
        {
            if (user == null) return false;
            int rowsAffected = ExecuteNonQueryCommand(ProcStarter + "CFN_Users_Update",
            CreateParameter("UserID", SqlDbType.Int, user.UserID),
            CreateParameter("RoleID", SqlDbType.Int, user.RoleID),
            CreateParameter("Initial", SqlDbType.NVarChar, user.Initials),
            CreateParameter("FullName", SqlDbType.NVarChar, user.FullName),
            CreateParameter("EmailAddress", SqlDbType.NVarChar, user.EmailAddress),
            CreateParameter("Mobile", SqlDbType.VarChar, user.Mobile),
            CreateParameter("Password", SqlDbType.VarChar, user.Password),
                CreateParameter("ModifiedBy", SqlDbType.Int, user.ModifiedBy),
                CreateParameter("ModifiedDate", SqlDbType.DateTime2, user.ModifiedDate),
            CreateParameter("IsSigningPartner", SqlDbType.Bit, user.IsSigningPartner),
            CreateParameter("Signature", SqlDbType.VarChar, user.Signature),
            CreateParameter("ArabicName", SqlDbType.VarChar, user.ArabicName),
            CreateParameter("IsActive", SqlDbType.Bit, user.IsActive)
           );

            return rowsAffected > 0;
        }

        public bool UpdateUserProfile(Users user)
        {
            if (user == null) return false;
            int rowsAffected = ExecuteNonQueryCommand(ProcStarter + "CFN_Users_UpdateProfile",
            CreateParameter("UserID", SqlDbType.Int, user.UserID),
            CreateParameter("FullName", SqlDbType.NVarChar, user.FullName),
            CreateParameter("EmailAddress", SqlDbType.NVarChar, user.EmailAddress),
            CreateParameter("Mobile", SqlDbType.VarChar, user.Mobile)
           );
            return rowsAffected > 0;
        }
        public bool UpdateUserResetPassword(Users user, bool WithUpdateProfile, bool resetpassword)
        {
            if (user == null) return false;
            int rowsAffected = ExecuteNonQueryCommand(ProcStarter + "CFN_Users_Update",
            CreateParameter("UserID", SqlDbType.Int, user.UserID),
            CreateParameter("RoleID", SqlDbType.Int, user.RoleID),
            CreateParameter("Initial", SqlDbType.NVarChar, user.Initials),
            CreateParameter("FullName", SqlDbType.NVarChar, user.FullName),
            CreateParameter("EmailAddress", SqlDbType.NVarChar, user.EmailAddress),
            CreateParameter("Mobile", SqlDbType.VarChar, user.Mobile),
            CreateParameter("Password", SqlDbType.VarChar, user.Password),
            CreateParameter("WithUpdateProfile", SqlDbType.Bit, WithUpdateProfile),
            CreateParameter("IsActive", SqlDbType.Bit, user.IsActive),
            CreateParameter("ResetPassword", SqlDbType.Bit, resetpassword));

            return rowsAffected > 0;
        }

        public bool DeleteUser(int userID)
        {
            int rowsEffected = ExecuteNonQueryCommand(ProcStarter + "CFN_Users_Delete", CreateParameter("UserID",
                SqlDbType.Int, userID));
            return rowsEffected > 0;
        }

        public int GetCurrentUsersCount()
        {
            object obj = ExecuteScalarCommand(ProcStarter + "CFN_Users_CurrentCount");
            return int.Parse(obj.ToString());
        }
        public bool AuthenticateUser(long userId, string password)
        {
            if (password == null) return false;
            DataRow dr = null;
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_UserPreferences_AuthenticateUser",
                CreateParameter("UserID", SqlDbType.BigInt, userId),
                CreateParameter("Password", SqlDbType.NVarChar, password));

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        dr = ds.Tables[0].Rows[0];
            }

            return dr != null;
        }
        public bool ResetPassword(long id, string newPassword)
        {
            int rowsEffected = ExecuteNonQueryCommand(ProcStarter + "CFN_Users_ResetPassword",
                CreateParameter("Password", SqlDbType.VarChar, newPassword),
                CreateParameter("UserID", SqlDbType.BigInt, id));

            return rowsEffected > 0;
        }
        public Users CreateUserFromDataRow(DataRow dr)
        {
            Users user = new Users
            {
                UserID = GetInt(dr, "UserID"),
                RoleID = GetInt(dr, "RoleID"),
                Initials = GetString(dr, "Initials"),
                FullName = GetString(dr, "FullName"),
                EmailAddress = GetString(dr, "EmailAddress"),
                Mobile = GetString(dr, "Mobile"),
                Password = GetString(dr, "Password"),
                ModifiedDate = GetDateTime(dr, "CreationDate"),
                LastLoginDate = GetDateTime(dr, "LastLoginDate"),
                LastPasswordChangeDate = GetDateTime(dr, "LastPasswordChangeDate"),
                IsActive = GetBool(dr, "IsActive"),
                IsSigningPartner = GetBool(dr, "IsSigningPartner"),
                Signature = GetString(dr, "Signature"),
                ArabicName = GetString(dr, "ArabicName")
            };

            if (dr.Table.Columns.Contains("RoleName"))
                user.RoleName = GetString(dr, "RoleName");

            if (dr.Table.Columns.Contains("Total"))
                user.Total = GetInt(dr, "Total");

            if (dr.Table.Columns.Contains("RowIndex"))
                user.RowIndex = GetInt(dr, "RowIndex");

            return user;
        }

        public List<Users> SelectAll(int skip, int next, string filterText, int UserID)
        {
            AuditAppLib lib = new AuditAppLib();
            if (filterText.Contains("~"))
            {
                filterText = filterText.Replace("~", "/");
                try
                {
                    filterText = lib.Date_SaveFormat(filterText);
                }
                catch (Exception ex)
                {

                }
            }
            List<Users> users = new List<Users>();
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Users_SelectAll",
                CreateParameter("Skip", SqlDbType.Int, skip),
                CreateParameter("Next", SqlDbType.Int, next),
                CreateParameter("UserID", SqlDbType.Int, UserID),
                CreateParameter("FilterText", SqlDbType.VarChar, filterText.Replace("|", "")));
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                return null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                users.Add(CreateUserFromDataRow(dr));
            }
            return users;
        }

        public List<Users> SearchUsersByUsername(string username, int loggedInUserID, int roleId, bool isSigningPartner)
        {
            List<Users> users = new List<Users>();
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Users_SearchByFullname",
                                            CreateParameter("FullName", SqlDbType.VarChar, username),
                                            CreateParameter("RoleID", SqlDbType.Int, roleId),
                                            CreateParameter("IsSigningPartner", SqlDbType.Bit, isSigningPartner));
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                return null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                users.Add(CreateUserFromDataRow(dr));
            }
            return users;
        }

        public bool IsLoginTicketAuthenticated(int userID, string loginTicket)
        {
            if (string.IsNullOrEmpty(loginTicket))
                return false;

            object obj = ExecuteScalarCommand(ProcStarter + "CFN_Users_IsLoginTicketAuthenticated",
                CreateParameter("UserID", SqlDbType.Int, userID),
                CreateParameter("LoginTicket", SqlDbType.VarChar, loginTicket));

            return obj != null && obj.Equals("1");
        }

        /// <summary>
        /// Used in logintech.aspx
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="expectedTotalUsers"></param>
        /// <returns></returns>
        public Users SelectUserForLogin(string userName, string password, int expectedTotalUsers = 0)
        {
            DataRow dr = null;
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Users_SelectForLoginByUsername",
                        CreateParameter("UserName", SqlDbType.NVarChar, userName),
                        CreateParameter("Password", SqlDbType.VarChar, password),
                        CreateParameter("ExpectedTotalUsers", SqlDbType.VarChar, expectedTotalUsers));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        dr = ds.Tables[0].Rows[0];
            }
            return CreateUserFromDataRow(dr);
        }

        /// <summary>
        /// Used for LoginID mapping in login.aspx
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="expectedTotalUsers"></param>
        /// <returns></returns>
        public Users SelectUserForLoginByUserID(int userID, string password, int expectedTotalUsers)
        {
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Users_SelectForLoginByUserID",
                                             CreateParameter("UserID", SqlDbType.Int, userID),
                                             CreateParameter("Password", SqlDbType.VarChar, password),
                                             CreateParameter("ExpectedTotalUsers", SqlDbType.VarChar, expectedTotalUsers));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return CreateUserFromDataRow(ds.Tables[0].Rows[0]);

            return null;
        }

        public bool ValidateUserOldPassword(int userID, string password)
        {
            bool result = false;
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Users_ValidateUserPassword",
                                             CreateParameter("UserID", SqlDbType.Int, userID),
                                             CreateParameter("Password", SqlDbType.VarChar, password));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                result = Convert.ToBoolean(ds.Tables[0].Rows[0]["Result"]);

            return result;
        }

        public bool AuthenticateMasterUser(int userID, string password)
        {
            bool result = false;
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Users_AuthenticateMasterUser",
                                             CreateParameter("UserID", SqlDbType.Int, userID),
                                             CreateParameter("Password", SqlDbType.VarChar, password));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                result = true;

            return result;
        }

        public bool CheckUsersInRoleExists(int roleID)
        {
            int countUsers = (int)ExecuteScalarCommand(ProcStarter + "CFN_Users_RoleExists", CreateParameter("RoleID", SqlDbType.Int, roleID));

            return countUsers > 0;
        }

        public bool IsValueDuplicate(string table, string column, string value, string groupColumn = "", string groupValue = "", string PrimaryColumn = "", string PrimaryColumnValue = "")
        {
            DataRow dr = null;

            DataSet ds = ExecuteQueryCommand(ProcStarter + "GLB_IsValueDuplicate",
                                             CreateParameter("Table", SqlDbType.VarChar, table),
                                             CreateParameter("Column", SqlDbType.VarChar, column),
                                             CreateParameter("Value", SqlDbType.VarChar, value.Trim()),
                                             CreateParameter("GroupColumn", SqlDbType.VarChar, groupColumn),
                                             CreateParameter("GroupValue", SqlDbType.VarChar, groupValue.Trim()),
                                             CreateParameter("PrimaryColumn", SqlDbType.VarChar, PrimaryColumn),
                                             CreateParameter("PrimaryColumnValue", SqlDbType.VarChar, PrimaryColumnValue.Trim()));
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
