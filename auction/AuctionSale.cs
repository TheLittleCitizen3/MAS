using MAS.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MAS.auction
{
    class AuctionSale : IAuctionSale
    {
        public IProduct ProductToSale { get ; set ; }
        public int Id { get ; set ; }
        public DateTime AuctionDate { get ; set ; }
        public ConcurrentStack<IBid> AgentBids { get ; set ; }
        public List<IAgent> AuctionAgents { get ; set ; }
        private object obj = new object();

        public event AuctionEvent StartAuction;
        public event AuctionEvent NewBidEvent;

        public AuctionSale(IProduct product)
        {
            ProductToSale = product;
            Id = (new Random()).Next(1, 500);
            AuctionDate = DateTime.Now;
            AgentBids = new ConcurrentStack<IBid>();
            AuctionAgents = new List<IAgent>();
        }

        public void NewBid(IBid bid)
        {
            IBid currentbid;
            lock (obj)
            {
                AgentBids.TryPop(out currentbid);
                if (bid.BidPrice > currentbid.BidPrice && bid.BidPrice >= ProductToSale.MinPriceRaise)
                {
                    AgentBids.Push(bid);
                    AnnounceNewBid(bid);
                }
                else
                {
                    AgentBids.Push(currentbid);
                }
            }
            
        }

        public void StartAuctionToAgents()
        {
            Output.print($"started auction: {Id}, with product: {ProductToSale.Id}");
            Output.print($"Product description: {ProductToSale.ProductDescription}");
            StartAuction.DynamicInvoke(this);

        }
        public void AnnounceNewBid(IBid newBid)
        {
            Output.print($"new bid was made by:{newBid.BidAgent.AgentId} with price: {newBid.BidPrice}");
            NewBidEvent.DynamicInvoke(this);

        }       


    }
}
