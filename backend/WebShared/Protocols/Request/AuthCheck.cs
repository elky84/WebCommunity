
namespace Web.Protocols.Request
{
    public class AuthCheck : RequestHeader
    {
        public override Id ProtocolId { get { return Id.AuthCheck; } }

        public string Token { get; set; }
    }
}
