using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TaskManager.Entities;
using TaskManager.Entities.GeneralModels;
using TaskManager.Services.GeneralServices;

namespace TaskManager.Services.CommonServices
{
    public class TasksBusinessService : ClientBaseServices
    {
        private string ProcStarter => "_";
        public int UserID { set; get; }

        public TasksBusinessService(string clientConnectionString)
            : base(clientConnectionString)
        {
        }
        public Tasks SelectRoleByRoleID(int taskId)
        {
            DataSet ds = ExecuteQueryCommand(ProcStarter + "Tasks_SelectByTaskId", 
                CreateParameter("TaskId", SqlDbType.Int, taskId));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return CreateTaskFromDataRow(ds.Tables[0].Rows[0]);
            return null;
        }
        public List<Tasks> SelectAll(int skip, int next, string filterText, int UserID)
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
            List<Tasks> tasks = new List<Tasks>();
            DataSet ds = ExecuteQueryCommand(ProcStarter + "Tasks_SelectAll",
                CreateParameter("Skip", SqlDbType.Int, skip),
                CreateParameter("Next", SqlDbType.Int, next),
                CreateParameter("UserID", SqlDbType.Int, UserID),
                CreateParameter("FilterText", SqlDbType.VarChar, filterText.Replace("|", "")));
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                return null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tasks.Add(CreateTaskFromDataRow(dr));
            }
            return tasks;
        }
        public DataTable SelectAll()
        {
            DataSet ds = ExecuteQueryCommand(ProcStarter + "Tasks_SelectAll");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public int InsertTask(Tasks task)
        {
            if (task == null) return 0;

            int newID = -1;

            SqlParameter newIDParameter = CreateParameter("NewTaskID", SqlDbType.Int, newID, ParameterDirection.Output);

            ExecuteNonQueryCommand(ProcStarter + "Tasks_Insert",
                CreateParameter("TaskTitle", SqlDbType.NVarChar, task.TaskTitle),
                CreateParameter("TaskDescription", SqlDbType.NVarChar, task.TaskDescription),
                CreateParameter("CreatedBy", SqlDbType.Int, task.CreatedBy),
                CreateParameter("CreatedDate", SqlDbType.DateTime2, task.CreatedDate),
                CreateParameter("IsActive", SqlDbType.Bit, task.IsActive), newIDParameter);

            return (int)newIDParameter.Value;
        }

        public bool UpdateTask(Tasks task)
        {
            if (task == null) return false;
            int rowsAffected = ExecuteNonQueryCommand(ProcStarter + "Tasks_Update",
            CreateParameter("TaskId", SqlDbType.VarChar, task.TaskId),
                CreateParameter("TaskTitle", SqlDbType.NVarChar, task.TaskTitle),
                CreateParameter("TaskDescription", SqlDbType.NVarChar, task.TaskDescription),
                CreateParameter("ModifiedBy", SqlDbType.Int, task.ModifiedBy),
                CreateParameter("ModifiedDate", SqlDbType.DateTime2, task.ModifiedDate),
            CreateParameter("IsActive", SqlDbType.Bit, task.IsActive));
            return (rowsAffected > 0);
        }

        public bool DeleteRole(int id)
        {
            int rowsEffected = ExecuteNonQueryCommand(ProcStarter + "CFN_Roles_Delete", CreateParameter("RoleID", SqlDbType.Int, id));

            return rowsEffected > 0;
        }


        public Tasks CreateTaskFromDataRow(DataRow dr)
        {
            Tasks task = new Tasks
            {
                TaskId = GetInt(dr, "TaskId"),
                TaskTitle = GetString(dr, "TaskTitle"),
                TaskDescription = GetString(dr, "TaskDescription"),
                IsActive = GetBool(dr, "IsActive")
            };
            return task;

        }
    }
}
