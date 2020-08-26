using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IStructure : IProduct
    {
        public List<IRoom> Rooms { get; set; }
        public bool isAccessible { get; set; }
        public int ToiletsRoom { get; set; }
        public int DinningRooms { get; set; }

    }
}
