using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Protocols.Common;
using Web.Types;

namespace Web.Protocols.Response
{
    public class SignUp : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.SignUp; } }

        public Account Account { get; set; }
    }
}
