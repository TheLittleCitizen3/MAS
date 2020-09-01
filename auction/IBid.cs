using System;

namespace MAS
{
    public interface IBid
    {
        public IAgent BidAgent { get; set; }
        public DateTime Bidtime { get; set; }
        public decimal BidPrice { get; set; }

    }
}
