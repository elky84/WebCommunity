
namespace Web.Protocols.Request
{
    public class Authenticate : RequestHeader
    {
        public override Id ProtocolId { get { return Id.Authenticate; } }

        public string UserId { get; set; }
    }
}
