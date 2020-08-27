using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IAgent
    {
        public int AgentId { get; set; }
        public decimal MoneyBalance { get; set; }
        public List<IAuctionSale> RegisteredAuctions { get; set; }
        public List<IProduct> CollectedProducts { get; set; }
        public void ChargeMoney(decimal price);
        public void AddProduct(IProduct product);
        public void EnterAuction(IAuctionSale auctionSale);
        public void MakeBid(IAuctionSale auctionSale);
        public bool ShouldJoinAuction(IAuctionSale auction);
    }
}
