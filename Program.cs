using MAS.auction;
using MAS.factories;
using MAS.MAS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            StructureFactory structfactory = new StructureFactory();
            AgentFactory agentFactory = new AgentFactory();
            List<IAuctionSale> auctionSales = new List<IAuctionSale>();
            List<IAgent> agents = new List<IAgent>();
            for (int i = 0; i < r.Next(2,5); i++)
            {
                agents.Add(agentFactory.GetAgent());
                auctionSales.Add(new AuctionSale(structfactory.GetStructure()));
            }
            IMas mas = new Mas(auctionSales, agents);
            
            Parallel.ForEach(auctionSales, (auction) =>
            {
                mas.AnnaounceAuction(auction);
                auction.StartAuctionToAgents();
                auction.CheckNewBids();
                auction.AnnounceWinner();

            });
            


        }
    }
}
