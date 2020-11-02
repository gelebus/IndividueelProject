using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderReference { get; set; }

        public string Adress { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
