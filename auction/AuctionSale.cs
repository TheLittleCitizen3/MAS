using MAS.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MAS.auction
{
    class AuctionSale : IAuctionSale
    {
        public IProduct ProductToSale { get; set; }
        public int Id { get; set; }
        public DateTime AuctionDate { get; set; }
        public ConcurrentStack<IBid> AgentBids { get; set; }
        public List<IAgent> AuctionAgents { get; set; }
        private int NewBidsCounter;
        private object obj = new object();

        public event AuctionEvent StartAuction;
        public event AuctionEvent NewBidEvent;
        

        public AuctionSale(IProduct product)
        {
            ProductToSale = product;
            Id = (new Random()).Next(1, 500);
            AuctionDate = DateTime.Now;
            AgentBids = new ConcurrentStack<IBid>();
            AgentBids.Push(new Bid(null, ProductToSale.StartPrice));
            AuctionAgents = new List<IAgent>();
            NewBidsCounter = 1;
        }

        public void NewBid(IBid bid)
        {
            IBid currentbid;
            lock (obj)
            {
                AgentBids.TryPeek(out currentbid);
                if ((bid.BidPrice - currentbid.BidPrice) >= ProductToSale.MinPriceRaise)
                {
                    AgentBids.Push(bid);
                }

            }

        }

        public void CheckNewBids()
        {
            
            if (AgentBids.Count > NewBidsCounter)
            {
                NewBidsCounter++;
                AnnounceNewBid();
            }
            else
            {
                ///TODO START FINISH AUCTION
            }

            
        }
        public void StartAuctionToAgents()
        {
            Output.print($"started auction: {Id}, with product: {ProductToSale.Id}");
            Output.print($"Product description: {ProductToSale.ProductDescription}");
            StartAuction.DynamicInvoke(this);

        }
        public void AnnounceNewBid()
        {
            IBid newBid = getLastBid();
            Output.print($"new bid was made by:{newBid.BidAgent.AgentId} with price: {newBid.BidPrice}");
            NewBidEvent.DynamicInvoke(this);

        }

        public void AnnounceWinner()
        {
            IAgent winningAgent = GetLastBidAgent();
            Output.print($"The Winner in auction: {Id}, is: {winningAgent.AgentId}");
            ChargeAgentOnProduct(winningAgent);
            winningAgent.AddProduct(ProductToSale);
            
        }
        private IAgent GetLastBidAgent()
        {
            IBid lastBid;
            AgentBids.TryPeek(out lastBid);
            return lastBid.BidAgent;
        }
        private void ChargeAgentOnProduct(IAgent agent)
        {
            decimal priceToCharge = GetLastBidPrice();
            agent.ChargeMoney(priceToCharge);
        }
        private decimal GetLastBidPrice()
        {
            IBid lastBid = getLastBid();
            if (lastBid == null)
            {
                return 0;
            }
            return lastBid.BidPrice;
        }
        private IBid getLastBid()
        {
            IBid LastBid;
            AgentBids.TryPeek(out LastBid);
            return LastBid;
        }


    }
}
