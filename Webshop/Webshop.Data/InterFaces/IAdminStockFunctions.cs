using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Data.InterFaces
{
    public interface IAdminStockFunctions
    {
        Task CreateStock(Stock stock);
        Task<IEnumerable<Stock>> UpdateStock(IEnumerable<Stock> stock);
        IEnumerable<Stock> GetStock(int productId);
        Task RemoveStock(int id);
    }
}
