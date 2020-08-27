using MAS.auction;
using MAS.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public void Start()
        {
            List<Task> AuctionTasks = new List<Task>();
            foreach (var auction in Auctions)
            {
                TimeSpan taskDelay = GetTaskDelay(auction);
                Task t = Task.Delay(taskDelay).ContinueWith(o => StartAuction(auction));
                AuctionTasks.Add(t);
            }
            Task.WaitAll(AuctionTasks.ToArray());
        }
        private TimeSpan GetTaskDelay(IAuctionSale auction)
        {
            if (auction.AuctionDate > DateTime.Now)
            {
                var timeDelay = (auction.AuctionDate - DateTime.Now).TotalSeconds;
                int secondsDelay = (int)timeDelay;
                return new TimeSpan(0, 0, secondsDelay);
            }
            else
            {
                return new TimeSpan(0, 0, 0);
            }
        }
        public void AnnaounceAuction(IAuctionSale auction)
        {
            Utils.RunEventAsync(AnouncceAgents, auction);
        }

        public void StartAuction(IAuctionSale auction)
        {
            AnnaounceAuction(auction);
            if (auction.AuctionAgents.Count > 0)
            {
                auction.RunAuction();
            }
            else
            {
                Output.print($"No one registered Auction: {auction.Id}");
            }
            
        }
        public IAuctionSale GetAuctionSchedule()
        {
            throw new NotImplementedException();
        }
    }
}
