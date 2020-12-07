using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Webshop.Interface
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<StockDTO> Stock { get; set; }
        public List<OrderProductDTO> OrderProducts { get; set; }
    }
}
