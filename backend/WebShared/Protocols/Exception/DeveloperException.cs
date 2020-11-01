using System.Net;

namespace Web.Protocols.Exception
{
    public class DeveloperException : System.Exception
    {
        public Web.Code.ResultCode ResultCode { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string Detail { get; set; }

        public DeveloperException(Web.Code.ResultCode resultCode, HttpStatusCode httpStatusCode = System.Net.HttpStatusCode.InternalServerError, string detail = null)
        {
            ResultCode = resultCode;
            HttpStatusCode = httpStatusCode;
            Detail = detail;
        }
    }
}
