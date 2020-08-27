using MAS.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.MAS
{
    class RegisterManager
    {
        private IAgent Agent;
        public RegisterManager(IAgent agent)
        {
            Agent = agent;
        }
        public void RegisterAgentsToAuction(IAuctionSale auction)
        {
            if (Agent.ShouldJoinAuction(auction))
            {
                Agent.EnterAuction(auction);
                AddToStartAuction(auction);
                AddToNewBid(auction);
                Output.print($"Agent: {Agent.AgentId} joinned auction: {auction.Id}");
            }
            
        }
        private void AddToStartAuction(IAuctionSale auction)
        {
            auction.StartAuction += Agent.MakeBid;
        }
        private void AddToNewBid(IAuctionSale auction)
        {
            auction.NewBidEvent += Agent.MakeBid;
        }

    }
}
