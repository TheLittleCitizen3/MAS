using MAS.agent;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public delegate void RegisterAgents(IAuctionSale auction);
    public interface IMas
    {
        public List<IAuctionSale> Auctions { get; set; }
        public List<IAgent> Agents { get; set; }
        public void AnnaounceAuction(IAuctionSale auction);
        public void StartAuction(IAuctionSale auction);
        public IAuctionSale GetAuctionSchedule();
        public event RegisterAgents AnouncceAgents;
        



    }
}
