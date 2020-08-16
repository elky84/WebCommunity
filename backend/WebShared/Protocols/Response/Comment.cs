namespace Web.Protocols.Response
{
    public class Comment : ResponseHeader
    {
        public override Id ProtocolId { get { return Id.Comment; } }

        public Common.Comment Data { get; set; }
    }
}
