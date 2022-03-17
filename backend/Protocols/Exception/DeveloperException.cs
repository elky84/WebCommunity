using System.Net;

namespace Protocols.Exception
{
    public class DeveloperException : System.Exception
    {
        public EzAspDotNet.Protocols.Code.ResultCode ResultCode { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string Detail { get; set; }

        public DeveloperException(EzAspDotNet.Protocols.Code.ResultCode resultCode, HttpStatusCode httpStatusCode = System.Net.HttpStatusCode.InternalServerError, string detail = null)
        {
            ResultCode = resultCode;
            HttpStatusCode = httpStatusCode;
            Detail = detail;
        }
    }
}
