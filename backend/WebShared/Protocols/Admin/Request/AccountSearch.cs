using System;
using System.Collections.Generic;
using System.Text;

using Web.Types;

namespace Web.Protocols.Admin.Request
{
    public class AccountSearch : RequestHeader
    {
        public override Id ProtocolId { get { return Id.AdminAccountSearch; } }

        public string UserId { get; set; }

        public AccountGradeType? Grade { get; set; }

        public AccountStateType? State { get; set; }

    }
}
