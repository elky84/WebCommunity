using System;
using System.Collections.Generic;
using System.Text;

using Web.Types;

namespace Web.Protocols.Admin.Request
{
    public class AccountSearch : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.AdminAccountSearch; } }

        public string UserId { get; set; }

        public AccountGradeType? Grade { get; set; }

        public AccountStateType? State { get; set; }

    }
}
