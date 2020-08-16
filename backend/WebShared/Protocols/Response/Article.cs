namespace Web.Protocols.Response
{
    public class Article : ResponseHeader
    {
        public override Id ProtocolId { get { return Id.Article; } }

        public Common.Article Data { get; set; }
    }
}
