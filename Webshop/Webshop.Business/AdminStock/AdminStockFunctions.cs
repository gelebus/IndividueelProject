using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Interface;
using Webshop.Data.Managers;
using Webshop.Logic.ViewModels;

namespace Webshop.Logic.AdminStock
{
    public class AdminStockFunctions
    {
        IAdminStockFunctions IAdminStockFunctions;
        public AdminStockFunctions(AppDbContext context)
        {
            IAdminStockFunctions = new StockManager(context);
        }

        public async Task<StockViewModel> RunCreateStock(StockViewModel request)
        {
            var stock = new Stock()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Description = request.Description
            };
            await IAdminStockFunctions.CreateStock(stock);

            return new StockViewModel() 
            { 
                StockId = stock.Id,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                Description = stock.Description
            };
        }
        public async Task<bool> RunRemoveStock(int id)
        {
            await IAdminStockFunctions.RemoveStock(id);
            return true;
        }
        public Task<IEnumerable<Stock>> RunUpdateStock(IEnumerable<StockViewModel> stockVms)
        {
            List<Stock> stock = new List<Stock>();

            foreach (StockViewModel stockViewModel in stockVms)
            {
                stock.Add(new Stock() 
                { 
                    Id = stockViewModel.StockId,
                    ProductId = stockViewModel.ProductId,
                    Quantity = stockViewModel.Quantity,
                    Description = stockViewModel.Description
                });
            }
            IAdminStockFunctions.UpdateStock(stock); 
            return (Task<IEnumerable<Stock>>)(IEnumerable<Stock>)stock;
        }
        public IEnumerable<AdminProductViewModel> RunGetStock()
        {
            var Stocks = IAdminStockFunctions.GetStock();
            List<AdminProductViewModel> stockvms = new List<AdminProductViewModel>();
            foreach(var stock in Stocks)
            {
                stockvms.Add(new AdminProductViewModel()
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Stock = stock.Stock
                });
            }   
            return stockvms;
        }
            

    }
}
