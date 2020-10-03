using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Protocols.Page
{
    public class PageResponse<T> : ResponseHeader
    {
        public long Total { get; set; }

        public List<T> Contents { get; set; }
    }
}
