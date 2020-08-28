using MAS.product;
using System;
using System.Collections.Generic;

namespace MAS.factories
{
    class StructureFactory
    {
        public IStructure GetStructure()
        {
            Random r = new Random();
            RoomFactory roomFactory = new RoomFactory();
            List<IRoom> rooms = new List<IRoom>();
            for (int i = 0; i < r.Next(1, 10); i++)
            {
                rooms.Add(roomFactory.GetRoom());
            }
            bool isAccessible = r.Next(1) > 0;
            int toiletssroom = r.Next(1, 5);
            int dinningRooms = r.Next(1, 3);
            List<string> roomsNames = new List<string>() { "Vered 50", "KAKAL13", "Rotshild22", "Hertzel2" };
            string productName = roomsNames[r.Next(3)];
            List<string> roomsDesc = new List<string>() { "Office with sea view", "Living house", "old appartment" };
            string productDescription = roomsDesc[r.Next(2)];
            decimal startPrice = (decimal)r.Next(1, 50);
            decimal minPriceRaise = startPrice / 10;
            int id = r.Next(1, 10000);
            return new Structure(rooms, isAccessible, toiletssroom, dinningRooms, productName, productDescription, startPrice, minPriceRaise, id);
        }
    }
}
