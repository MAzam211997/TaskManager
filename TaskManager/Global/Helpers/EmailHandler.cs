using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Shark.PdfConvert;
using TaskManager.ViewModel;

namespace TaskManager.Global.Helpers
{
    public interface IEmailHandler
    {
        bool SendEmail(SendEmailModel model);
        bool SendEmailWithAttachment(SendEmailModel model);
    }
    public class EmailHandler : IEmailHandler
    {

        public bool SendEmail(SendEmailModel model)
        {
            var fromAddress = new MailAddress(model.StrFrom, model.DisplayName);
            var toAddress = new MailAddress(model.StrTo);

            MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = model.StrSubject,
                Body = model.StrBody,
                IsBodyHtml = true,
            };

            using (var smtp = new SmtpClient
            {
                Host = model.SmtpServer,
                Port = model.SmtpPort,
                EnableSsl = model.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(model.UserName, model.Password)
            })
            {
                try
                {
                    smtp.Send(message);
                    message.Attachments.Dispose();
                }
                catch (Exception exception)
                {
                    message.Attachments.Dispose();
                    return false;
                }
            }
            return true;
        }

        public bool SendEmailWithAttachment(SendEmailModel model)
        {
            var str = new StringBuilder("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"/></head><body>");
            str.Append(model.StrBody);
            str.Append("</body></html>");

            PdfConversionSettings config = new PdfConversionSettings
            {
                PdfToolPath = Directory.GetCurrentDirectory() + "\\wwwroot\\wkhtmltopdf\\bin\\wkhtmltopdf.exe",
                Content = str.ToString(),
            };

            var name = model.StrSubject.Split("of")[1].Trim();

            string fileName = name + "_" + DateTime.Now.ToString("yyMdhhmm");

            //Documents folder created if not exist
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\downloads\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\wwwroot\\downloads\\");
            }

            using (var fileStream = new FileStream(Directory.GetCurrentDirectory() + "\\wwwroot\\downloads\\" + fileName + ".pdf", FileMode.Create))
            {
                PdfConvert.Convert(config, fileStream);
                fileStream.Close();
            }

            var fromAddress = new MailAddress(model.StrFrom, model.DisplayName);
            var toAddress = new MailAddress(model.StrTo);

            MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = model.StrSubject,
                Body = "Please find the attached file" + Environment.NewLine,
                IsBodyHtml = false,
                Attachments = { new Attachment(Directory.GetCurrentDirectory() + "\\wwwroot\\downloads\\" + fileName + ".pdf", MediaTypeNames.Application.Pdf) }
            };

            using (var smtp = new SmtpClient
            {
                Host = model.SmtpServer,
                Port = model.SmtpPort,
                EnableSsl = model.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(model.UserName, model.Password)
            })
            {
                try
                {
                    smtp.Send(message);
                    message.Attachments.Dispose();
                    File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\downloads\\" + fileName + ".pdf");
                }
                catch (Exception exception)
                {
                    message.Attachments.Dispose();
                    File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\downloads\\" + fileName + ".pdf");
                    return false;
                }
            }
            return true;
        }

        public static Stream Base64ToImageStream(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            return ms;
        }
    }
}

