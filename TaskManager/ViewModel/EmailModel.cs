using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ViewModel
{
    public class EmailModel
    {
        public string EmailAddress { get; set; }
        public string RecordNumber { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string UserName { get; set; }
        public bool HasAttachment { get; set; }
        public EmailType EmailType { get; set; }
    }

    public enum EmailType
    {
        Default,
        Reminder,
        Notify
    }

    public class SendEmailModel
    {
        public string StrFrom { get; set; }
        public string StrSubject { get; set; }
        public string StrBody { get; set; }
        public string StrTo { get; set; }
        public string DisplayName { get; set; }
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RecordNumber { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public byte[] HBytes { get; set; }

    }


}

