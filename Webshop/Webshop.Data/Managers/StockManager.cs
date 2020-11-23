using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data.InterFaces;

namespace Webshop.Data.Managers
{
    public class StockManager:IAdminStockFunctions
    {
        private AppDbContext _context;

        public StockManager(AppDbContext context)
        {
            _context = context;
        }
        async Task IAdminStockFunctions.CreateStock(Stock stock)
        {
            _context.Stock.Add(stock);
            await _context.SaveChangesAsync();
        }
        IEnumerable<Stock> IAdminStockFunctions.GetStock(int productId)
        {
            var stock = _context.Stock.Where(a => a.ProductId == productId).ToList();
            return stock;
        }
        async Task IAdminStockFunctions.RemoveStock(int id)
        {
            var stock = _context.Stock.FirstOrDefault(a => a.Id == id);
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
        }
        async Task<IEnumerable<Stock>> IAdminStockFunctions.UpdateStock(IEnumerable<Stock> stock)
        {
            _context.UpdateRange(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}
