﻿namespace MAS
{
    public interface IRoom
    {
        public int Id { get; set; }
        public decimal Size { get; set; }
        public bool HasCooling { get; set; }
        public bool ProtectedRoom { get; set; }


    }
}
