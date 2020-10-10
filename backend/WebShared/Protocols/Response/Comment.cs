namespace Web.Protocols.Response
{
    public class Comment : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.Comment; } }

        public Common.Comment Data { get; set; }
    }
}
