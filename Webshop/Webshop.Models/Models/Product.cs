using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Webshop.ModelLib.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
    }
}
