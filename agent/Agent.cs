using MAS.auction;
using MAS.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.agent
{
    class Agent : IAgent
    {
        public int AgentId { get ; set ; }
        public double MoneyBalance { get; set ; }
        public List<IAuctionSale> RegisteredAuctions { get; set; }

        public Agent(double moneyBalance)
        {
            MoneyBalance = moneyBalance;
            AgentId = (new Random()).Next(1, 500);
            RegisteredAuctions = new List<IAuctionSale>();
        }
        public void EnterAuction(IAuctionSale auctionSale)
        {
            RegisteredAuctions.Add(auctionSale);
        }


        public void MakeBid(IAuctionSale auctionSale)
        {
            if (IsMakeBid(auctionSale))
            {
                auctionSale.NewBid(new Bid(this, GetLastBidPrice(auctionSale) + auctionSale.ProductToSale.MinPriceRaise));
            }
        }

        private bool IsMakeBid(IAuctionSale auction)
        {

            return GetLastBidPrice(auction) <= MoneyBalance;
        }
        public bool ShouldJoinAuction(IAuctionSale auction)
        {
            return auction.ProductToSale.StartPrice < MoneyBalance;
        }

        private double GetLastBidPrice(IAuctionSale auctionSale)
        {
            IBid bid;
            auctionSale.AgentBids.TryPeek(out bid);
            return bid.BidPrice;
        }
    }
}
