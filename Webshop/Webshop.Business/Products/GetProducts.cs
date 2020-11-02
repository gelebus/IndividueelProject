﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webshop.Data;
using Webshop.Logic.ViewModels;

namespace Webshop.Logic.Products
{
    public class GetProducts
    {
        private AppDbContext _context;

        public GetProducts(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Run()
        {
            IEnumerable<ProductViewModel> products = _context.Products.ToList().Select(a => new ProductViewModel
            {
                Description = a.Description,
                Value = $"€{a.Value.ToString("N2")}",
                Name = a.Name
            });
            return products;
        }
    }
}
