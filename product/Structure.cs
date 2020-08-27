using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.product
{
    class Structure : IStructure
    {
        public List<IRoom> Rooms { get ; set ; }
        public bool isAccessible { get ; set ; }
        public int ToiletsRoom { get ; set ; }
        public int DinningRooms { get ; set ; }
        public string ProductName { get ; set ; }
        public string ProductDescription { get ; set ; }
        public decimal StartPrice { get ; set ; }
        public decimal MinPriceRaise { get ; set ; }
        public int Id { get ; set ; }

        public Structure(List<IRoom> rooms,
            bool isAccessible,
            int toiletssroom,
            int dinningRooms,
            string productName,
            string productDescription,
            decimal startPrice,
            decimal minPriceRaise,
            int id)
        {
            this.isAccessible = isAccessible;
            ToiletsRoom = toiletssroom;
            DinningRooms = dinningRooms;
            ProductName = productName;
            ProductDescription = productDescription;
            StartPrice = startPrice;
            MinPriceRaise = minPriceRaise;
            Id = id;
        }
    }
}
