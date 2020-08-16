namespace Web.Protocols.Exception
{
    public class ErrorDetails : ResponseHeader
    {
        public int StatusCode { get; set; }

        public string Detail { get; set; }

    }
}
