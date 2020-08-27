using MAS.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace MAS.auction
{
    class AuctionSale : IAuctionSale
    {
        public IProduct ProductToSale { get; set; }
        public int Id { get; set; }
        public DateTime AuctionDate { get; set; }
        public ConcurrentStack<IBid> AgentBids { get; set; }
        public List<IAgent> AuctionAgents { get; set; }
        public Status AuctionStatus { get; set; }
        private IBid CurrentBid;
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
            CurrentBid = new Bid(null, 1);
            AuctionStatus = Status.PendingStart;
        }

        public void RunAuction()
        {
            StartAuctionToAgents();
            while (AuctionStatus == Status.Running)
            {
                CallForNewBid();
                CheckNewBids();
                if (AuctionStatus == Status.FinalCall)
                {
                    FinalCall();
                }
            }
            if (getLastBid().BidAgent != null)
            {
                AnnounceWinner();
            }
            else
            {
                Output.print($"There Is No Winner in Auction: {Id}, no Bids Where Made");
            }
            
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
                    Output.print($"New bid was made by:{bid.BidAgent.AgentId} with price: {bid.BidPrice} at Auction: {Id}");
                }

            }

        }
        public void CheckNewBids()
        {
            
            if (getLastBid() != CurrentBid)
            {
                CurrentBid = getLastBid();
                AuctionStatus = Status.Running;
            }
            else
            {
                AuctionStatus = Status.FinalCall;
            }
            
        }
        public void FinalCall()
        {
            int counter = 1;
            while (AuctionStatus == Status.FinalCall && counter <= 3)
            {
                Output.print($"Final Call: {counter}, on auction: {Id}");
                CallForNewBid();
                CheckNewBids();
                counter++;
                Thread.Sleep(1000);
            }
            if (AuctionStatus == Status.FinalCall)
            {
                AuctionStatus = Status.Finished;
            }
        }
        public void StartAuctionToAgents()
        {
            Output.print($"started auction: {Id}, with product ID: {ProductToSale.Id} and Name: {ProductToSale.ProductName}");
            Output.print($"Product description: {ProductToSale.ProductDescription}");
            Output.print($"The Start price of the product: {ProductToSale.StartPrice} and minimum raise is: {ProductToSale.MinPriceRaise}");
            Output.print("-------------------------------------------------------------------");
            AuctionStatus = Status.Running;
            //StartAuction?.DynamicInvoke(this);
            Utils.RunEventAsync(StartAuction, this);

        }
        public void CallForNewBid()
        {
            //NewBidEvent?.DynamicInvoke(this);
            Utils.RunEventAsync(NewBidEvent, this);
        }
        public void AnnounceWinner()
        {
            IAgent winningAgent = GetLastBidAgent();
            Output.print($"The Winner in auction: {Id}, is: {winningAgent.AgentId}");
            ChargeAgentOnProduct(winningAgent);
            winningAgent.AddProduct(ProductToSale);
            AuctionStatus = Status.Finished;
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
