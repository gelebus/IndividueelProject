using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Logic.ViewModels;
using Webshop.Interface;
using Webshop.Data.Managers;
using Webshop.Data;

namespace Webshop.Logic.Stock
{
    public class Stock
    {
        private int Id;
        private int Quantity;
        private string Description;
        private int ProductId;
        private IStock Istock;

        public Stock(StockViewModel vm, string conString)
        {
            Id = vm.Id;
            Quantity = vm.Quantity;
            Description = vm.Description;
            ProductId = vm.ProductId;
            Istock = Factory.Factory.CreateIStock(conString);
        }

        public void RunUpdate()
        {
            Istock.UpdateStock(new StockDTO() 
            { 
                Id = Id,
                Quantity = Quantity,
                Description = Description,
                ProductId = ProductId
            });
        }
    }
}
