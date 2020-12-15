using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Webshop.Interface
{
    public interface IStockFunctions
    {
        StockDTO CreateStock(StockDTO stock);
        IEnumerable<StockResponse> GetStock();
        void RemoveStock(int id);

        public class StockResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockDTO> Stock { get; set; }
        }
    }
}
