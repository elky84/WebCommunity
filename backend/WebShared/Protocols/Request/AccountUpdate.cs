
namespace Web.Protocols.Request
{
    public class AccountUpdate : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.AccountUpdate; } }

        public string Password { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }
    }
}
