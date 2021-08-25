using System.Net;
namespace TaskManager.ViewModel
{
    public class ResponseViewModel
    {
        public object _obj { get; set; }
        public HttpStatusCode _statusCode { get; set; }
        public string _message { get; set; }
        public ResponseViewModel()
        {

        }
        public ResponseViewModel(HttpStatusCode httpStatusCode, object obj = null, string message = null)
        {
            _obj = obj;
            _statusCode = httpStatusCode;
            _message = message;
        }
    }
}
