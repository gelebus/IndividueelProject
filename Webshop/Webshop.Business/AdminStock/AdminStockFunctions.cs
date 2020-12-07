﻿using System;
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
                StockId = stock.Id,
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
                stock.Add(new StockDTO() 
                { 
                    Id = stockViewModel.StockId,
                    ProductId = stockViewModel.ProductId,
                    Quantity = stockViewModel.Quantity,
                    Description = stockViewModel.Description
                });
            }
            IAdminStockFunctions.UpdateStock(stock); 
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
                        StockId = s.Id,
                        Description = s.Description,
                        Quantity = s.Quantity
                    });
                }
                productvms.Add(new AdminProductViewModel()
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Stock = stockvms
                });
            }   
            return productvms;
        }
            

    }
}
