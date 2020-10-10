using System;
using System.Collections.Generic;
using System.Text;


namespace Web.Protocols.Request
{
    public class WebSocketAuth : RequestHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.WebSocketAuth; } }

        public string Cookie { get; set; }
    }
}
