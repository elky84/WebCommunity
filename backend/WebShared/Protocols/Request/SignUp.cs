
namespace Web.Protocols.Request
{
    public class SignUp : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.SignUp; } }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }
    }
}
