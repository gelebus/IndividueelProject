using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Webshop.Interface
{
    public interface IAdminStockFunctions
    {
        Stock CreateStock(Stock stock);
        void UpdateStock(IEnumerable<Stock> stock);
        IEnumerable<StockResponse> GetStock();
        void RemoveStock(int id);

        public class StockResponse
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<Stock> Stock { get; set; }
        }
    }
}
