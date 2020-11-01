
namespace Web.Protocols.Request
{
    public class SignIn : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.SignIn; } }

        public string UserId { get; set; }

        public string Password { get; set; }
    }
}
