using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskManager.Global;
using TaskManager.Global.Helpers;
using TaskManager.Managers;
using GeneralHelper = TaskManager.Entities.GeneralModels.GeneralHelper;
using TaskManager.ViewModel;
using TaskManager.Entities;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        //private SmartAppUserManager userManager;
        private ResponseViewModel responseViewModel;
        private readonly IConfiguration _configuration;

        private readonly IEmailHandler _emailHandler;
        public AccountController(IConfiguration configuration, IEmailHandler emailHandler)
        {
            _configuration = configuration;
            _emailHandler = emailHandler;

        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            SessionVars.ConnectionString = _configuration.GetConnectionString("ConnectionString");

            if (!ModelState.IsValid)
            {
                return Ok(responseViewModel = new ResponseViewModel(HttpStatusCode.BadRequest, null, "Something went wrong.<br/>please try again"));
            }

            try
            {
                if (IsClientDbOnline(SessionVars.ConnectionString))
                {
                    CommonManager commonManager = new CommonManager();
                    Users userObject = commonManager.UserManager.SelectUserForLogin(loginViewModel.EmailAddress,
                        GeneralHelper.HashSHA1(loginViewModel.Password));

                    if (userObject == null)
                    {
                        return Ok(responseViewModel = new ResponseViewModel(HttpStatusCode.NotFound, null,
                            "Username or Password is wrong."));
                    }

                    // User Info
                    //The user with in the company
                    SessionVars.LoggedInLoginID = userObject.UserID;
                    SessionVars.LogedInInitial = userObject.Initials;
                    SessionVars.LoggedInRoleID = userObject.RoleID;
                    SessionVars.LoggedInUserDisplayName = userObject.FullName;
                    SessionVars.LogedInUserEmail = userObject.EmailAddress;
                    SessionVars.LogedInUserMobile = userObject.Mobile;

                    responseViewModel = new ResponseViewModel(HttpStatusCode.OK, userObject, "Success");

                }
                else
                {
                    responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, null, "<b>Company License Expired!</b> <br/> Please contact with system administrator.");
                }

            }
            catch (SqlException exc)
            {
                responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, null)
                {
                    _message = string.IsNullOrEmpty(exc.Message) ? "Unable to connect database" : exc.Message
                };
            }
            catch (Exception exe)
            {
                Console.WriteLine(exe);
                throw;
            }
            return Ok(responseViewModel);

        }

        public bool IsClientDbOnline(string connectionString)
        {
            using (var clientDbConnection = new SqlConnection(connectionString))
            {
                try
                {
                    clientDbConnection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        [HttpPost("AuthenticateForGotPasswordUser")]
        public IActionResult AuthenticateForGotPasswordUser([FromBody] LoginViewModel loginViewModel)
        {
            SessionVars.ConnectionString = _configuration.GetConnectionString("ConnectionString");

            if (!ModelState.IsValid)
            {
                return Ok(responseViewModel = new ResponseViewModel(HttpStatusCode.BadRequest, null, "Something went wrong.<br/>please try again"));
            }

            try
            {

                if (IsClientDbOnline(SessionVars.ConnectionString))
                {
                    CommonManager commonManager = new CommonManager();
                    Users userObject = commonManager.UserManager.SelectUserForGotPasswordByUserName(loginViewModel.EmailAddress);

                    if (userObject == null)
                    {
                        return Ok(responseViewModel = new ResponseViewModel(HttpStatusCode.NotFound, null,
                            "Username  is wrong."));
                    }

                    string _password = Guid.NewGuid().ToString().ToLower().Substring(0, 8);
                    userObject.Password = GeneralHelper.HashSHA1(_password);

                    bool isreset = commonManager.UserManager.UpdateUserResetPassword(userObject, false, true); ;
                    if (isreset)
                    {
                        bool _isEmailSent = SendAccountActivationEmail(userObject, _password);

                        responseViewModel = new ResponseViewModel(HttpStatusCode.OK, (_isEmailSent == true), "Success! Email Sent To your Email");
                    }
                    else
                    {
                        responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, "Success! Email Sent To your Email");
                    }

                }
                else
                {
                    responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, null, "Something Went Wrong");
                }

            }
            catch (SqlException exc)
            {
                responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, null)
                {
                    _message = string.IsNullOrEmpty(exc.Message) ? "Unable to connect database" : exc.Message
                };
            }
            catch (Exception exe)
            {
                Console.WriteLine(exe);
                throw;
            }
            return Ok(responseViewModel);

        }

        private bool SendAccountActivationEmail(Users user, string password = "")
        {
            bool _isEmailSent = false;
            try
            {

                if (user != null)
                {
                    string reportBody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>BANODI Newsletter</title>
    <style>
        @media (max-width:700px) {
            #table {
                min-width: inherit !important;
                width: 100%;
            }
        }
    </style>
</head>
<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0' style='font-family:'Open Sans','HelveticaNeue-Light','Helvetica Neue Light','Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif !important;'>
    <center>
        <table bgcolor='#eaeaea' align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%'>
            <tbody>
                <tr>
                    <td align='center' valign='top'>
                        <br />
                        <br />
                        <div>
                            <table style='max-width:600px; box-shadow: 0 3px 5px 0 rgba(0,0,0,0.3); border-radius: 5px; margin-bottom: 20px;' border='0' cellpadding='0' cellspacing='0'>
                                <tbody>
                                    <tr>
                                        <td align='center' valign='top'></td>
                                    </tr>
                                    <tr>
                                        <td align='center' valign='top'>

                                            <table bgcolor='#fff' border='0' cellpadding='15' cellspacing='0' width='100%' style='border-top:0px solid #3b677b; border-radius: 0 0 5px 5px;'>
                                                <tbody>
                                                    <tr>
                                                        <td style=' background-color: #2775ff; ' width='100%'>
                                                            <table cellpadding='0' cellspacing='0' width='100%'>
                                                                <tr>
                                                                    <td width='50%' style='padding:0;text-align: left; padding: 15px;'>
                                                                        <img src='https://demo.bnody.com/assets/img/login-eng-logo.svg' alt='Welcome to Bnody ERP' width='120'/>
                                                                    </td>
                                                                    <td width='50%' align='right' style='text-align:right; color: #fff; font-size: 13px;'>
                                                                        Date: " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style='background-color: #2775ff; text-align: center; padding: 0px 5px 20px 5px; color: #fff; font-size: 20px;'>
                                                            Reset password request
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='center' valign='top'></td>
                                                    </tr>
                                                    <tr mc:repeatable='mc:repeatable'>
                                                        <td align='left' valign='top' bgcolor='#fff' style='padding-bottom:0 ; font-family: 'Open Sans','HelveticaNeue-Light','Helvetica Neue Light','Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; font-size: 14px; font-weight: normal;'>
                                                            Hello <strong>" + user.FullName + @"</strong>
                                                        </td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td align='center' valign='top' style='border-bottom: 1px solid #e6e6e6; padding-bottom:5px;'></td>
                                                    </tr>
                                                    <tr>
                                                        <td align='left' valign='top' style='padding-bottom:0px; padding-top:20px; font-weight:bold;'><strong>New Password Details</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td align='center' valign='top' style='padding-top:0;'>
                                                            <table bgcolor='#fff' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-top:0px solid #3b677b; border-radius: 0 0 5px 5px; padding-bottom: 20px;'>
                                                                <tbody>                                                                                                                                     
                                                                    <tr>
                                                                        <td style='font-size: 13px; padding-top: 10px;'>Login ID:</td>
                                                                        <td style='font-size: 13px; padding-top: 10px;'>" + user.EmailAddress + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style='font-size: 13px; padding-top: 10px;'>New Password:</td>
                                                                        <td style='font-size: 13px; padding-top: 10px;'>" + password + @"</td>
                                                                    </tr>                                                                    
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>                                                                                                        
                                                    <tr>
                                                        <td align='center' valign='top' style='border-bottom: 1px solid #e6e6e6; padding-bottom:5px;'></td>
                                                    </tr>
                                                    <tr>
                                                        <td align='left' valign='top' style='padding-bottom:0px; padding-top:20px; font-weight:bold;'><strong>Message from BNODY</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td style='font-family: 'Open Sans','HelveticaNeue-Light','Helvetica Neue Light','Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif;font-size: 13px;line-height: 1.8;padding-bottom: 25px;'>
                                                            Thank you for registering on our website https://www.bnody.com/.
                                                            Bnody ERP is a Cloud Software for Small & Medium Enterprises. Bnody ERP is an advanced ERP software solution that sets up and organizes main turn-arounds in a process to coordinate with technology, resources and assets of an enterprise that includes a great ERP Software.
                                                            Automation of enterprise operation with Bnody ERP adds value in terms of improving results. It is a reliable ERP software solution of web based applications.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style='background: #f2f2f2' align='center'>
                                                            <table id='table' style='min-width:600px; max-width: 600px;  margin-bottom: 20px; text-align: center;' border='0' cellpadding='0' cellspacing='0'>
                                                                <tbody>

                                                                    <tr>
                                                                        <td style='padding-top:20px; border-bottom: 1px solid #e6e6e6; padding-bottom:25px;'>
                                                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                                                                <tr>
                                                                                    <td style='font-size: 13px; padding-bottom: 10px; text-align: left; width: 50%;'>
                                                                                        <strong style='font-weight:600;'>Email:</strong> info@bnody.com
                                                                                    </td>
                                                                                    <td style='font-size: 13px; padding-bottom: 10px; text-align: right; width: 50%;'>
                                                                                        <strong style='font-weight:600;'>Phone:</strong> 92 001 1469
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan='2' style='font-size: 13px; padding-bottom: 0px; width: 50%; text-align: left;'>
                                                                                        <strong style='font-weight:600;'>
                                                                                            Address:
                                                                                        </strong> Prince Meteb Street, Rabwah District, Riyadh, KSA
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='center' valign='top'></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td align='center' valign='top'>
                                                                            <table border='0' cellpadding='15' cellspacing='0' width='100%' style='border-radius: 5px; border-top:0px solid #1b75bb; padding-bottom: 10px; text-align: center;'>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style='font-size: 13px; padding-bottom: 0px;'>
                                                                                            © 2021 Bnody. All Rights Reserved.
                                                                                        </td>
                                                                                    </tr>                                                                                    
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                        <br />
                        <br />
                    </td>
                </tr>
            </tbody>
        </table>
    </center>
</body>
</html>";

                    bool response = true;

                    SendEmailModel sn = new SendEmailModel
                    {
                        StrFrom = _configuration["SMTPFrom"],
                        StrTo = user.EmailAddress,
                        StrSubject = _configuration["Subject"],
                        StrBody = reportBody,
                        DisplayName = _configuration["DisplayName"],
                        SmtpServer = _configuration["SMTPServer"],
                        SmtpPort = Convert.ToInt32(_configuration["SMTPPort"]),
                        UserName = _configuration["SMTPUsername"],
                        Password = _configuration["SMTPPassword"],
                        EnableSsl = Convert.ToBoolean(_configuration["SMTPEnableSsl"]),
                        HBytes = null,
                        //RecordNumber = model.RecordNumber
                    };

                    var fromAddress = new MailAddress(sn.StrFrom, sn.DisplayName);
                    var toAddress = new MailAddress(sn.StrTo);

                    MailMessage message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = sn.StrSubject,
                        Body = sn.StrBody,
                        IsBodyHtml = true
                    };
                    //*Attachments = { new Attachment(Directory.GetCurrentDirectory() + "\\wwwroot\\downloads\\" + fileName+".pdf") }
                    //message.AlternateViews.Add(alterView);
                    using (var smtp = new SmtpClient
                    {
                        Host = sn.SmtpServer,
                        Port = sn.SmtpPort,
                        EnableSsl = sn.EnableSsl,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sn.UserName, sn.Password),
                        Timeout = 10000,
                    })
                    {
                        try
                        {
                            smtp.Send(message);
                            message.Attachments.Dispose();
                            _isEmailSent = true;
                        }
                        catch (Exception exception)
                        {
                            message.Attachments.Dispose();
                            response = false;
                            return false;
                        }
                    }

                    responseViewModel = response ? new ResponseViewModel(HttpStatusCode.OK, null, "2351") : new ResponseViewModel(HttpStatusCode.InternalServerError, null, "66");
                }
            }
            catch (Exception ex)
            {
                _isEmailSent = false;
            }
            return _isEmailSent;
        }




    }
}
