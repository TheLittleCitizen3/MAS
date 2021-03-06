﻿using MAS.auction;
using System;
using System.Collections.Generic;

namespace MAS.agent
{
    class Agent : IAgent
    {
        public int AgentId { get; set; }
        public decimal MoneyBalance { get; set; }
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
            if (IsMakeBid(auctionSale) && auctionSale.AuctionStatus == Status.Running)
            {
                auctionSale.NewBid(new Bid(this, GetLastBidPrice(auctionSale) + auctionSale.ProductToSale.MinPriceRaise));
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
            IAgent lastBidAgent = GetLastBidaAgent(auction);
            if (GetLastBidPrice(auction) < MoneyBalance && lastBidAgent != this)
            {
                bool makebid = (new Random()).Next(100) > 45;
                return makebid;
            }
            return false;
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
        private IAgent GetLastBidaAgent(IAuctionSale auctionSale)
        {
            IBid bid;
            auctionSale.AgentBids.TryPeek(out bid);
            return bid.BidAgent;
        }
    }
}
