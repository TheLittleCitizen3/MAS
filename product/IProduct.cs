using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IProduct
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal StartPrice { get; set; }
        public decimal MinPriceRaise { get; set; }
        public int Id { get; set; }
    }
}
