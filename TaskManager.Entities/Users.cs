using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Entities
{
    public class Users: AuditableEntity
    {
        public int UserID { get; set; }
        public String Username { get; set; }
        public String Fullname { get; set; }
        public String Email { get; set; }
        public String ContactNo { get; set; }
        public String Password { get; set; }
        public String Initials { get; set; }
    }
}
