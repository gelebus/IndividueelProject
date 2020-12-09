using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Interface
{
    public interface IStock
    {
        void UpdateStock(StockDTO stock);
    }
}
