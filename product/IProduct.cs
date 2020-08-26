using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IProduct
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double StartPrice { get; set; }
        public double MinPriceRaise { get; set; }
        public int Id { get; set; }
    }
}
