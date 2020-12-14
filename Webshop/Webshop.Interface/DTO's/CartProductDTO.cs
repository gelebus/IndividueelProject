using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Interface
{
    public class CartProductDTO
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}
