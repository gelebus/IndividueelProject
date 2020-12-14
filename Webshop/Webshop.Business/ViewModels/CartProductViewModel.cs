using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Logic
{
    public class CartProductViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}
