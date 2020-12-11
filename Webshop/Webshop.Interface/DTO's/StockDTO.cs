using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Interface
{
    public class StockDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
    }
}
