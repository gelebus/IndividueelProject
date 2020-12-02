using Microsoft.EntityFrameworkCore;
using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Interface;

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
        IEnumerable<IAdminStockFunctions.StockResponse> IAdminStockFunctions.GetStock()
        {
            var stock = _context.Products
                .Include(a => a.Stock)
                .Select(a => new IAdminStockFunctions.StockResponse
                {
                    Id = a.Id,
                    Description = a.Description,
                    Stock = a.Stock.Select(b => new Stock
                    {
                        Id = b.Id,
                        Description = b.Description,
                        Quantity = b.Quantity
                    })
                })
                .ToList(); 

            return stock;
        }
        async Task IAdminStockFunctions.RemoveStock(int id)
        {
            var stock = _context.Stock.FirstOrDefault(a => a.Id == id);
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
        }
        async Task IAdminStockFunctions.UpdateStock(IEnumerable<Stock> stock)
        {
            _context.UpdateRange(stock);
            await _context.SaveChangesAsync();
        }
    }
}
