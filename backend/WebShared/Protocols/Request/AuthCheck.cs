
namespace Web.Protocols.Request
{
    public class AuthCheck : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.AuthCheck; } }

        public string Token { get; set; }
    }
}
