using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Protocols.Response
{
    public class AuthCheck : ResponseHeader
    {
        public override Id ProtocolId { get { return Id.AuthCheck; } }

        public string UserId { get; set; }
    }
}
