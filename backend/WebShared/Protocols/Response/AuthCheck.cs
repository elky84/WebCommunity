using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Protocols.Response
{
    public class AuthCheck : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.AuthCheck; } }

        public string UserId { get; set; }
    }
}
