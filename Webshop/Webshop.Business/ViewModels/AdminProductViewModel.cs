using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Logic.ViewModels
{
    public class AdminProductViewModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<StockViewModel> Stock { get; set; }
    }
}
