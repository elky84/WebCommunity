using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Protocols.Page
{
    public class PageResponse<T> : ResponseHeader
    {
        public int Total { get; set; }

        public List<T> Contents { get; set; }

        public PageResponse(List<T> contents, int total)
        {
            Contents = contents;
            Total = total;
        }
    }
}
