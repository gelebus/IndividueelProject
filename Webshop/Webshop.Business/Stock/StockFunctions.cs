using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Interface;
using Webshop.Data.Managers;
using Webshop.Logic.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Logic.Stock
{
    public class StockFunctions
    {
        private readonly string ConString;
        IStockFunctions IAdminStockFunctions;
        IStock iStock;
        public StockFunctions(string conString, IStock stock, IStockFunctions stockFunctions)
        {
            if (stock != null || stockFunctions != null)
            {
                iStock = stock;
                IAdminStockFunctions = stockFunctions;
            }
            else
            {
                ConString = conString;
                IAdminStockFunctions = Factory.Factory.CreateIStockFunctions(ConString);
                iStock = Factory.Factory.CreateIStock(conString);
            }
                
        }

        public StockViewModel RunCreateStock(StockViewModel request)
        {
            var stock = new StockDTO()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Description = request.Description
            };
            stock = IAdminStockFunctions.CreateStock(stock);

            return new StockViewModel() 
            { 
                Id = stock.Id,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                Description = stock.Description
            };
        }
        public bool RunRemoveStock(int id)
        {
            IAdminStockFunctions.RemoveStock(id);
            return true;
        }
        public IEnumerable<StockDTO> RunUpdateStock(IEnumerable<StockViewModel> stockVms)
        {
            List<StockDTO> stock = new List<StockDTO>();
            foreach (StockViewModel stockViewModel in stockVms)
            {
                new Stock(stockViewModel, iStock).RunUpdate();
                stock.Add(new StockDTO()
                {
                    Id = stockViewModel.Id,
                    ProductId = stockViewModel.ProductId,
                    Quantity = stockViewModel.Quantity,
                    Description = stockViewModel.Description
                });
            }
            return stock;
        }
        public IEnumerable<AdminProductViewModel> RunGetStock()
        {
            var Stocks = IAdminStockFunctions.GetStock();
            List<AdminProductViewModel> productvms = new List<AdminProductViewModel>();
            
            foreach(var stock in Stocks)
            {
                List<StockViewModel> stockvms = new List<StockViewModel>();
                foreach(var s in stock.Stock)
                {
                    stockvms.Add(new StockViewModel()
                    {
                        ProductId = s.ProductId,
                        Id = s.Id,
                        Description = s.Description,
                        Quantity = s.Quantity
                    });
                }
                productvms.Add(new AdminProductViewModel()
                {
                    Id = stock.Id,
                    Name = stock.Name,
                    Description = stock.Description,
                    Stock = stockvms
                });
            }   
            return productvms;
        }
            

    }
}
