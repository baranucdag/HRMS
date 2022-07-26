using System.Net;

namespace Core.Utilities.Result
{
    public class ResultItem
    {
        public bool IsOk { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string ProcessCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ResultItem(bool isOk = true, object data = null, string message = "", HttpStatusCode httpStatusCode = HttpStatusCode.OK, string processCode = "")
        {
            this.IsOk = isOk;
            this.Data = data;
            this.Message = message;
            this.StatusCode = httpStatusCode;
            this.ProcessCode = processCode;
        }
    }
}
