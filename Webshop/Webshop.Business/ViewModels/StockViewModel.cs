using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Logic.ViewModels
{
    public class StockViewModel
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
