using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Data.InterFaces;
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

        public async Task<StockViewModel> CreateStock(StockViewModel request)
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
        public async Task<bool> RemoveStock(int id)
        {
            await IAdminStockFunctions.RemoveStock(id);
            return true;
        }
        public IEnumerable<Stock> UpdateStock(IEnumerable<StockViewModel> stockVms)
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
            IEnumerable<Stock> response = (IEnumerable<Stock>)IAdminStockFunctions.UpdateStock(stock); //list oplossing eventueel
            return response;
        }
        public IEnumerable<StockViewModel> GetStock(int productId)
        {
            IEnumerable<Stock> stockList = IAdminStockFunctions.GetStock(productId);
            List<StockViewModel> stockvms = new List<StockViewModel>();
            foreach(var stock in stockList)
            {
                stockvms.Add(new StockViewModel()
                {
                    StockId = stock.Id,
                    ProductId = stock.ProductId,
                    Quantity = stock.Quantity,
                    Description = stock.Description
                });
            }
            return stockvms;
        }
            

    }
}
