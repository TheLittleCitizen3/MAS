using MAS.product;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace MAS.factories
{
    class RoomFactory
    {
        public IRoom GetRoom()
        {
            Random r = new Random();
            int id = r.Next(1, 10000);
            decimal size = (decimal)r.Next(1, 999);
            bool hasCooling = r.Next(1) > 0;
            bool protectedRoom = r.Next(1) > 0;
            return new Room(id, size, hasCooling, protectedRoom);
        }
    }
}
