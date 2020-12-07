using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Webshop.Interface
{
    public interface IAdminStockFunctions
    {
        StockDTO CreateStock(StockDTO stock);
        void UpdateStock(IEnumerable<StockDTO> stock);
        IEnumerable<StockResponse> GetStock();
        void RemoveStock(int id);

        public class StockResponse
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockDTO> Stock { get; set; }
        }
    }
}
