using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Protocols.Common;
using Web.Types;

namespace Web.Protocols.Response
{
    public class SignIn : ResponseHeader
    {
        public override ProtocolId ProtocolId { get { return ProtocolId.SignIn; } }

        public Common.Account Account { get; set; }
    }
}
