using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Webshop.ModelLib.Models
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Stock> Stock { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
