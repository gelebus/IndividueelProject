using System;
using System.Collections.Generic;
using System.Text;
using Webshop.ModelLib.Models;

namespace ModelLib.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
