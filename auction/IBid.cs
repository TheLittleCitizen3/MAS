using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IBid
    {
        public IAgent BidAgent { get; set; }
        public DateTime Bidtime { get; set; }
        public double BidPrice { get; set; }

    }
}
