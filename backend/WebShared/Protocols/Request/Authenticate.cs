
namespace Web.Protocols.Request
{
    public class Authenticate : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.Authenticate; } }

        public string UserId { get; set; }
    }
}
