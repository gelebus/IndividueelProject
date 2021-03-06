﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Webshop.Factory;
using Webshop.Interface;
using Webshop.Logic.ViewModels;

namespace Webshop.Logic.Products
{
    public class Product
    {
        private int Id;
        private decimal Value;
        private string Name;
        private string Description;
        private IEnumerable<StockViewModel> Stock;
        IProduct adminProductFunctions;

        public Product(AdminProductViewModel vm, IProduct iProduct)
        {
            Id = vm.Id;
            Value = vm.Value;
            Name = vm.Name;
            Description = vm.Description;
            Stock = vm.Stock;    
            adminProductFunctions = iProduct;
        }

        public ProductDTO RunUpdate()
        {
            if(Stock == null)
            {
                return adminProductFunctions.UpdateProduct(new ProductDTO
                {
                    Name = Name,
                    Id = Id,
                    Value = Value,
                    Description = Description
                });
            }
            List<StockDTO> stock = new List<StockDTO>();
            foreach(var s in Stock)
            {
                stock.Add(new StockDTO 
                { 
                    Id = s.Id,
                    Description = s.Description,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                });
            }
            return adminProductFunctions.UpdateProduct(new ProductDTO
            {
                Name = Name,
                Id = Id,
                Value = Value,
                Description = Description,
                Stock = stock
            });
        }
        
    }
}
