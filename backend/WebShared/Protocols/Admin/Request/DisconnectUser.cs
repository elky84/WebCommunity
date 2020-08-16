using System;
using System.Collections.Generic;
using System.Text;


namespace Web.Protocols.Admin.Request
{
    public class DisconnectUser : RequestHeader
    {
        public override Id ProtocolId { get { return Id.AdminDisconnectUser; } }

        public string UserId { get; set; }
    }
}
