using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Interface
{
    public class OrderProductDTO
    {
        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
    }
}
