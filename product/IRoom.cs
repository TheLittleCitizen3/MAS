using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IRoom
    {
        public int Id { get; set; }
        public double Size { get; set; }
        public bool HasCooling { get; set; }
        public bool ProtectedRoom { get; set; }


    }
}
