using System.Collections.Generic;

namespace MAS
{
    public delegate void RegisterAgents(IAuctionSale auction);
    public interface IMas
    {
        public List<IAuctionSale> Auctions { get; set; }
        public List<IAgent> Agents { get; set; }
        public void Start();
        public void AnnaounceAuction(IAuctionSale auction);
        public void StartAuction(IAuctionSale auction);
        public event RegisterAgents AnouncceAgents;




    }
}
