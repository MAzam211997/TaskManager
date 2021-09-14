using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Global;
using TaskManager.Global.Filters;
using TaskManager.ViewModel;

namespace TaskManager.Controllers
{
    [SessionTimeout]
    [Route("api/Tasks")]
    public class TasksController : BaseController
    {
        private IWebHostEnvironment _hostingEnvironment;
        public TasksController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("[action]/{pq_curPage}/{pq_rPP}/{filterText}")]
        public IActionResult SelectAll(int pq_curPage, int pq_rPP, string filterText)
        {
            try
            {
                var tasks = CommonManager.TasksManager.SelectAll(pq_curPage, pq_rPP, filterText, SessionVars.LoggedInLoginID);
                _responseViewModel = tasks != null ? new ResponseViewModel(HttpStatusCode.OK, tasks) : new ResponseViewModel(HttpStatusCode.NotFound, tasks, "Not Found");
            }
            catch (Exception ex)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, null, ex.Message);
            }
            return Ok(_responseViewModel);
        }


        [HttpPost("SaveRec")]
        public IActionResult SaveRec(Tasks task)
        {
            try
            {
                task.CreatedBy = SessionVars.LoggedInLoginID;
                object result;
                if (task.IsInserting)
                {
                    result = CommonManager.TasksManager.InsertTask(task);
                }
                else
                {
                    result = CommonManager.TasksManager.UpdateTask(task);
                }

                if (task.IsInserting)
                {
                    _responseViewModel = _responseMessages.DisplayInsertMessage(result, "Data Saved Successfully.", "Something went wrong.");
                }
                else
                {
                    _responseViewModel = _responseMessages.DisplayUpdateMessage(result, "Data Updated Successfully.", "Something went wrong.");
                }
            }
            catch (SqlException e)
            {
                _responseViewModel = new ResponseViewModel(HttpStatusCode.InternalServerError, e, e.Message);
            }


            return Ok(_responseViewModel);
        }



    }
}
