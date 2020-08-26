using MAS.auction;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.MAS
{
    class Mas : IMas
    {
        public List<IAuctionSale> Auctions { get; set; }
        public List<IAgent> Agents { get; set; }
        public event RegisterAgents AnouncceAgents;

        public Mas(List<IAuctionSale> auctions,List<IAgent> agents)
        {
            Auctions = auctions;
            Agents = agents;
            Agents.ForEach(a => AnouncceAgents += new RegisterManager(a).RegisterAgentsToAuction);
        }

        public void AnnaounceAuction(IAuctionSale auction)
        {
            AnouncceAgents.DynamicInvoke(auction);
        }

        public void StartAuction(IAuctionSale auction)
        {
            throw new NotImplementedException();
        }
        public IAuctionSale GetAuctionSchedule()
        {
            throw new NotImplementedException();
        }
    }
}
