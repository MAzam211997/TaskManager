using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.ViewModel;

namespace TaskManager.Global.Filters
{
    public class SessionTimeout : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session == null ||
                !context.HttpContext.Session.TryGetValue("ConnectionString", out byte[] val) || SessionVars.ConnectionString == "")
            {
                context.Result = new BadRequestObjectResult(new ResponseViewModel { _statusCode = HttpStatusCode.Unauthorized, _message = "Session Expired", _obj = null });
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}