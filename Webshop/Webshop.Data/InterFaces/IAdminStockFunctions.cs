using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data.Managers;

namespace Webshop.Data.InterFaces
{
    public interface IAdminStockFunctions
    {
        Task CreateStock(Stock stock);
        Task UpdateStock(IEnumerable<Stock> stock);
        IEnumerable<StockManager.GetStockResponse> GetStock();
        Task RemoveStock(int id);
    }
}
