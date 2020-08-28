namespace MAS.product
{
    class Room : IRoom
    {
        public int Id { get; set; }
        public decimal Size { get; set; }
        public bool HasCooling { get; set; }
        public bool ProtectedRoom { get; set; }

        public Room(int id, decimal size, bool hasCooling, bool protectedRoom)
        {
            Id = id;
            Size = size;
            HasCooling = hasCooling;
            ProtectedRoom = protectedRoom;
        }
    }
}
