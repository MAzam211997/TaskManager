using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Entities
{
    public class Users: AuditableEntity
    {
        public int UserID { get; set; }
        public int RoleID { set; get; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Initials { get; set; }
        public string RoleName { set; get; }
        public string FullName { set; get; }
        public string EmailAddress { set; get; }
        public string Mobile { set; get; }
        public string CurrentPassword { set; get; }
        public DateTime LastLoginDate { set; get; }
        public DateTime LastPasswordChangeDate { set; get; }
        public bool IsSigningPartner { set; get; }
        public string Signature { set; get; }
        public string ArabicName { set; get; }
        public IFormFile Profile_Imae { set; get; }
    }
}
