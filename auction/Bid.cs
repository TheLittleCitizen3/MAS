using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.auction
{
    class Bid : IBid
    {
        public IAgent BidAgent { get ; set ; }
        public DateTime Bidtime { get ; set ; }
        public double BidPrice { get ; set ; }

        public Bid(IAgent bidAgent, double bidPrice)
        {
            BidAgent = bidAgent;
            Bidtime = DateTime.Now;
            BidPrice = bidPrice;
        }
    }
}
