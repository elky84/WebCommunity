using System;
using System.Collections.Generic;
using System.Text;


namespace Web.Protocols.Request
{
    public class WebSocketAuth : RequestHeader
    {
        public override Id ProtocolId { get { return Id.WebSocketAuth; } }

        public string Cookie { get; set; }
    }
}
