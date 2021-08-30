using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Entities.GeneralModels
{
    public class Roles
    {
        public int RoleID { set; get; }

        public string RoleName { set; get; }
        public int UserID { set; get; }

        public bool IsActive { set; get; }
        public bool IsInserting { set; get; }

        public override string ToString()
        {
            string text = "" +
           "IsActive : " + IsActive +
           "RoleID : " + RoleID +
           "RoleName : " + RoleName +
           "UserID : " + UserID;
            return text;
        }
    }
}
