using MAS.auction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MAS
{
    public delegate void AuctionEvent(IAuctionSale auction);
    public interface IAuctionSale
    {
        public IProduct ProductToSale { get; set; }
        public int Id { get; set; }
        public DateTime AuctionDate { get; set; }
        public ConcurrentStack<IBid> AgentBids { get; set; }
        public List<IAgent> AuctionAgents { get; set; }
        public void RunAuction();
        public void NewBid(IBid bid);
        public void StartAuctionToAgents();
        public void CheckNewBids();
        public void AnnounceWinner();
        public Status AuctionStatus { get; set; }

        public event AuctionEvent StartAuction;
        public event AuctionEvent NewBidEvent;


    }
}
