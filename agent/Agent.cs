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
        public decimal MoneyBalance { get; set ; }
        public List<IAuctionSale> RegisteredAuctions { get; set; }
        public List<IProduct> CollectedProducts { get; set; }

        public Agent(decimal moneyBalance)
        {
            MoneyBalance = moneyBalance;
            AgentId = (new Random()).Next(1, 500);
            RegisteredAuctions = new List<IAuctionSale>();
            CollectedProducts = new List<IProduct>();
        }
        public void EnterAuction(IAuctionSale auctionSale)
        {
            RegisteredAuctions.Add(auctionSale);
        }


        public void MakeBid(IAuctionSale auctionSale)
        {
            if (IsMakeBid(auctionSale))
            {
                Console.WriteLine($"gonna make bid by: {AgentId}");
                auctionSale.NewBid(new Bid(this, GetLastBidPrice(auctionSale) + auctionSale.ProductToSale.MinPriceRaise));
            }
            else
            {
                Console.WriteLine($"not gonna make bid {AgentId}");
            }
        }
        public void ChargeMoney(decimal price)
        {
            MoneyBalance -= price;
        }
        public void AddProduct(IProduct product)
        {
            CollectedProducts.Add(product);
        }

        private bool IsMakeBid(IAuctionSale auction)
        {

            return GetLastBidPrice(auction) < MoneyBalance;
        }
        public bool ShouldJoinAuction(IAuctionSale auction)
        {
            return auction.ProductToSale.StartPrice < MoneyBalance;
        }

        private decimal GetLastBidPrice(IAuctionSale auctionSale)
        {
            IBid bid;
            auctionSale.AgentBids.TryPeek(out bid);
            return bid.BidPrice;
        }
    }
}
