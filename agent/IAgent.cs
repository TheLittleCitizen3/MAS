using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IAgent
    {
        public int AgentId { get; set; }
        public double MoneyBalance { get; set; }
        public List<IAuctionSale> RegisteredAuctions { get; set; }
        public void EnterAuction(IAuctionSale auctionSale);
        public void MakeBid(IAuctionSale auctionSale);
        public bool ShouldJoinAuction(IAuctionSale auction);
    }
}
