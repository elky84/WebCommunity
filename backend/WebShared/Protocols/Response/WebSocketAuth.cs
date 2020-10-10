using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Protocols.Response
{
    public class WebSocketAuth : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.WebSocketAuth; } }
    }
}
