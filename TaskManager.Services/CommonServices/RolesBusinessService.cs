using System.Data;
using System.Data.SqlClient;
using TaskManager.Entities;
using TaskManager.Entities.GeneralModels;
using TaskManager.Services.GeneralServices;

namespace TaskManager.Services.CommonServices
{
    public class RolesBusinessService : ClientBaseServices
    {
        private string ProcStarter => "_";
        public int UserID { set; get; }

        public RolesBusinessService(string clientConnectionString)
            : base(clientConnectionString)
        {
        }
        public Roles SelectRoleByRoleID(int roleID)
        {

            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Roles_SelectByRoleID", CreateParameter("RoleID", SqlDbType.Int, roleID));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return CreateRoleFromDataRow(ds.Tables[0].Rows[0]);
            return null;
        }

        public DataTable SelectAll()
        {
            DataSet ds = ExecuteQueryCommand(ProcStarter + "CFN_Roles_SelectAll");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public int InsertRole(Roles role)
        {
            if (role == null) return 0;

            int newID = -1;

            SqlParameter newIDParameter = CreateParameter("NewRoleID", SqlDbType.Int, newID, ParameterDirection.Output);

            ExecuteNonQueryCommand(ProcStarter + "CFN_Roles_Insert",
                CreateParameter("RoleName", SqlDbType.VarChar, role.RoleName),
                CreateParameter("IsActive", SqlDbType.Bit, role.IsActive),
                CreateParameter("UserID", SqlDbType.Int, role.UserID), newIDParameter);

            return (int)newIDParameter.Value;
        }

        public bool UpdateRole(Roles role)
        {
            if (role == null) return false;
            int rowsAffected = ExecuteNonQueryCommand(ProcStarter + "CFN_Roles_Update",
            CreateParameter("RoleID", SqlDbType.VarChar, role.RoleID),
            CreateParameter("RoleName", SqlDbType.VarChar, role.RoleName),
            CreateParameter("IsActive", SqlDbType.Bit, role.IsActive),
            CreateParameter("UserID", SqlDbType.Int, role.UserID));
            return (rowsAffected > 0);
        }

        public bool DeleteRole(int id)
        {
            int rowsEffected = ExecuteNonQueryCommand(ProcStarter + "CFN_Roles_Delete", CreateParameter("RoleID", SqlDbType.Int, id));

            return rowsEffected > 0;
        }


        public Roles CreateRoleFromDataRow(DataRow dr)
        {
            Roles role = new Roles
            {
                RoleID = GetInt(dr, "RoleID"),
                RoleName = GetString(dr, "RoleName"),
                IsActive = GetBool(dr, "IsActive"),
                UserID = GetInt(dr, "UserID")
            };
            return role;

        }
    }
}
