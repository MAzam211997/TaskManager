using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using TaskManager.Global;
using TaskManager.Global.Filters;
using TaskManager.Managers;
using TaskManager.ViewModel;
using TaskManager.Entities;
using TaskManager.Entities.GeneralModels;

namespace TaskManager.Controllers
{
    [SessionTimeout]
    [Route("api/Users")]

    public class UsersController : BaseController
    {
        private IWebHostEnvironment _hostingEnvironment;
        public UsersController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("[action]/{pq_curPage}/{pq_rPP}/{filterText}")]
        public IActionResult SelectAll(int pq_curPage, int pq_rPP, string filterText)
        {
            try
            {
                var users = CommonManager.UsersManager.SelectAll(pq_curPage, pq_rPP, filterText, SessionVars.LoggedInLoginID);
                _responseViewModel = users != null ? new ResponseViewModel(HttpStatusCode.OK, users) : new ResponseViewModel(HttpStatusCode.NotFound, users, "Not Found");
            }
            catch (Exception ex)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, null, ex.Message);
            }
            return Ok(_responseViewModel);
        }

        [HttpDelete("[action]/{userID}/{fileName}")]
        public IActionResult DeleteRec(string userID, string fileName)
        {
            try
            {
                bool isRecordDeleted = false;
                if (!IsFileDelete(fileName))
                    isRecordDeleted = CommonManager.UsersManager.DeleteUser(Convert.ToInt32(userID));

                _responseViewModel = isRecordDeleted
                    ? new ResponseViewModel(HttpStatusCode.OK, true, "User is deleted successfully.")
                    : new ResponseViewModel(HttpStatusCode.NotFound, null, "User not found.");
            }
            catch (SqlException e)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, e, e.Message);
            }

            return Ok(_responseViewModel);
        }

        [HttpGet("LoadRec/{userID}")]
        public IActionResult LoadRec(int userID)
        {
            try
            {
                Users user = CommonManager.UsersManager.SelectUserByUserID(userID);
                _responseViewModel = new ResponseViewModel(HttpStatusCode.OK, user, "Success");
            }
            catch (Exception e)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, e, e.Message);
            }

            return Ok(_responseViewModel);
        }


        [HttpGet("SelectAllRoles")]
        public IActionResult SelectAllRoles()
        {
            DataTable roles = new DataTable();
            try
            {
                List<Roles> rolesList = new List<Roles>();
                roles = CommonManager.RolesManager.SelectAll();
                _responseViewModel = new ResponseViewModel(HttpStatusCode.OK, roles, "224");
            }
            catch (Exception e)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, e, e.Message);
            }

            return Ok(roles);
        }

        [HttpPost("SaveRec")]
        public IActionResult SaveRec()
        {
            try
            {
                var data = HttpContext.Request.Form;

                var isfileUpload = IsFileUpload(data);

                var user = new Users()
                {
                    UserID = Convert.ToInt32(data["UserID"]),
                    FullName = data["FullName"],
                    EmailAddress = data["EmailAddress"],
                    Initials = data["Initials"],
                    RoleID = Convert.ToInt32(data["RoleID"]),
                    Mobile = data["Mobile"],
                    Password = data["Password"],
                    CurrentPassword = data["CurrentPassword"],
                    IsInserting = Convert.ToBoolean(data["IsInserting"]),
                    IsActive = Convert.ToBoolean(data["IsActive"]),
                    IsSigningPartner = Convert.ToBoolean(data["IsSigningPartner"]),
                    Signature = data["Signature"],
                    ArabicName = data["ArabicName"]
                };

                if (user.IsInserting)
                {
                    if (CredentialManager.UserCredentialManager.IsValueDuplicate("CFN_Users", "EmailAddress", user.EmailAddress))
                        return Ok(new ResponseViewModel(HttpStatusCode.NotFound, false, "Email address already exist."));
                }

                if (user.CurrentPassword != user.Password)
                {
                    user.Password = GeneralHelper.HashSHA1(user.Password);
                    user.LastPasswordChangeDate = DateTime.UtcNow;
                }

                if (!user.IsSigningPartner)
                {
                    user.Signature = "";
                    user.ArabicName = "";
                }

                user.CreatedBy = SessionVars.LoggedInLoginID;
                object result;
                if (user.IsInserting)
                {
                    result = CommonManager.UsersManager.InsertUser(user);
                }
                else
                {
                    result = CommonManager.UsersManager.UpdateUser(user);
                }

                if (user.IsInserting)
                {
                    _responseViewModel = Convert.ToInt32(result) != -1
                        ? new ResponseViewModel(HttpStatusCode.OK, result, "Data Save Successfully")
                        : new ResponseViewModel(HttpStatusCode.InternalServerError, result, "Something went wrong.");
                }
                else
                {
                    _responseViewModel = Convert.ToBoolean(result)
                        ? new ResponseViewModel(HttpStatusCode.OK, result, "Data Update Successfully")
                        : new ResponseViewModel(HttpStatusCode.InternalServerError, result, "Something went wrong.");
                }
            }
            catch (SqlException e)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, e, e.Message);
            }


            return Ok(_responseViewModel);
        }

        private bool IsFileUpload(IFormCollection form)
        {
            IFormFile file = null;
            string path = "";
            string fileName = "";
            string fullPath = "";

            if (form.Files.Count > 0)
            {
                file = form.Files[0];
                path = Path.Combine(_hostingEnvironment.WebRootPath, "SignatureDocuments");
                fileName = Path.GetFileName(file.FileName);
                fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "SignatureDocuments/" + fileName);

                //Documents folder created if not exist
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }


            if (Convert.ToBoolean(form["IsInserting"]))
            {
                //check if file is exist or not 
                if (file != null && file.Length > 0)
                {
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                else
                {
                    return false;
                }

                return new FileInfo(fullPath).Exists;
            }
            else
            {
                var oldFile = form["OldFile"];
                var newFile = form["Name"];

                if (oldFile != newFile)
                {
                    //delete old file
                    IsFileDelete(oldFile);

                    //check if file is exist or not 
                    if (file != null && file.Length > 0)
                    {
                        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }

                    return new FileInfo(fullPath).Exists;
                }
                else
                {
                    fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "SignatureDocuments/" + newFile);

                    return new FileInfo(fullPath).Exists;
                }
            }
        }

        private bool IsFileDelete(string fileName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "SignatureDocuments/" + fileName);

            FileInfo file = new FileInfo(path);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
                file.Refresh();
            }

            return file.Exists;
        }


        [HttpPost("UpdateProfile")]
        public IActionResult UpdateProfile([FromBody] Users user)
        {
            try
            {
                if (user.IsInserting)
                {
                    if (CredentialManager.UserCredentialManager.IsValueDuplicate("CFN_Users", "EmailAddress", user.EmailAddress))
                        return Ok(new ResponseViewModel(HttpStatusCode.NotFound, false, "Email address already exist."));
                }

                user.Password = GeneralHelper.HashSHA1(user.Password);
                user.CreatedBy = SessionVars.LoggedInLoginID;
                object result = null;
                if (!user.IsInserting)
                {
                    if (CredentialManager.UserCredentialManager.IsValueDuplicate("CFN_Users", "EmailAddress", user.EmailAddress))
                        return Ok(new ResponseViewModel(HttpStatusCode.NotFound, false, "Email address already exist."));

                    result = CommonManager.UsersManager.UpdateUserProfile(user);
                    _responseViewModel = Convert.ToBoolean(result)
                        ? new ResponseViewModel(HttpStatusCode.OK, result, "Profile Updated Successfully")
                        : new ResponseViewModel(HttpStatusCode.InternalServerError, result, "Something went wrong.");
                }
            }
            catch (SqlException e)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, e, e.Message);
            }


            return Ok(_responseViewModel);
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] PasswordResetModel resetModel)
        {
            try
            {
                bool isPasswordChanged =
                    CommonManager.UsersManager.ResetPassword(resetModel.LoginID, GeneralHelper.HashSHA1(resetModel.Password));

                _responseViewModel = isPasswordChanged
                    ? new ResponseViewModel(HttpStatusCode.OK, true, "Success")
                    : new ResponseViewModel(HttpStatusCode.NotFound, null, "Data not found");
            }
            catch (Exception e)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, e, e.Message);
            }

            return Ok(_responseViewModel);
        }
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel resetModel)
        {
            try
            {

                if (!CommonManager.UsersManager.AuthenticateUser(SessionVars.LoggedInLoginID, GeneralHelper.HashSHA1(resetModel.OldPassword)))
                {
                    return Ok(new ResponseViewModel(HttpStatusCode.NotFound, false, "Your current password is wrong."));
                }
                if (resetModel.NewPassword != resetModel.ConfirmNewPassword)
                {
                    return Ok(new ResponseViewModel(HttpStatusCode.NotFound, false, "New password and confirm password should be same."));
                }
                bool isPasswordChanged = CommonManager.UsersManager.ResetPassword(SessionVars.LoggedInLoginID, GeneralHelper.HashSHA1(resetModel.NewPassword));

                _responseViewModel = isPasswordChanged
                    ? new ResponseViewModel(HttpStatusCode.OK, true, "Success")
                    : new ResponseViewModel(HttpStatusCode.NotFound, null, "Data not found");
            }
            catch (Exception e)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, e, e.Message);
            }

            return Ok(_responseViewModel);
        }

        [HttpGet("{tableNames}")]
        public IActionResult UserSearch(string tableNames)
        {
            string key = "";
            int loggedInUserID = 0, roleId = 0;
            bool isSigningPartner = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(Request.Query["search_key"]))
                {
                    key = Request.Query["search_key"];
                    roleId = Convert.ToInt32(Request.Query["roleId"]);
                    isSigningPartner = Convert.ToBoolean(Request.Query["isSigningPartner"]);
                    var namesObj = CommonManager.UsersManager.SearchUsersByUsername(key, loggedInUserID, roleId, isSigningPartner);
                    if (namesObj != null)
                    {
                        return Ok(namesObj);
                    }
                    else
                    {
                        List<Users> lstUserNames = new List<Users>();

                        Users objreturn = new Users();
                        objreturn.UserID = 0;
                        objreturn.FullName = "No result found";
                        lstUserNames.Add(objreturn);
                        return Ok(lstUserNames);
                    }
                }
            }
            catch (Exception ex)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok(null);
        }
    }
}
