using System;

namespace MAS.auction
{
    class Bid : IBid
    {
        public IAgent BidAgent { get; set; }
        public DateTime Bidtime { get; set; }
        public decimal BidPrice { get; set; }

        public Bid(IAgent bidAgent, decimal bidPrice)
        {
            BidAgent = bidAgent;
            Bidtime = DateTime.Now;
            BidPrice = bidPrice;
        }
    }
}
