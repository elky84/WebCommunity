namespace Web.Protocols.Response
{
    public class Article : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.Article; } }

        public Common.Article Data { get; set; }
    }
}
