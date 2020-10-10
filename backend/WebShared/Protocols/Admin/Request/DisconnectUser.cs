using System;
using System.Collections.Generic;
using System.Text;


namespace Web.Protocols.Admin.Request
{
    public class DisconnectUser : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.AdminDisconnectUser; } }

        public string UserId { get; set; }
    }
}
