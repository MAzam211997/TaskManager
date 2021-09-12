using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaskManager.ViewModel;

namespace TaskManager.Global
{
    public class ResponseMessages
    {
         ResponseViewModel _responseViewModel = new ResponseViewModel();
        public ResponseViewModel DisplayInsertMessage(object result, string successMessage, string failedMessage)
        {
            return _responseViewModel = Convert.ToInt32(result) != -1
                 ? new ResponseViewModel(HttpStatusCode.OK, result, successMessage)
                 : new ResponseViewModel(HttpStatusCode.InternalServerError, result, failedMessage);
        }
        public ResponseViewModel DisplayUpdateMessage(object result, string successMessage, string failedMessage)
        {
            return _responseViewModel = Convert.ToBoolean(result)
                 ? new ResponseViewModel(HttpStatusCode.OK, result, successMessage)
                 : new ResponseViewModel(HttpStatusCode.InternalServerError, result, failedMessage);
        }
    }
    
}
