using System.Collections.Generic;

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
