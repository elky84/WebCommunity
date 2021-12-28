using EzAspDotNet.Protocols;
using EzAspDotNet.Protocols.Id;
using Protocols.Types;

namespace Protocols.Admin.Request
{
    public class AccountSearch : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return Id.ProtocolId.AdminAccountSearch; } }

        public string UserId { get; set; }

        public AccountGradeType? Grade { get; set; }

        public AccountStateType? State { get; set; }

    }
}
