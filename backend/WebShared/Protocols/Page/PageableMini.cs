using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Protocols.Page
{
    public class PageableMini
    {
        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 20;
    }
}
