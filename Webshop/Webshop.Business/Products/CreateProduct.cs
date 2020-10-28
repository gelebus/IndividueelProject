using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Data;
using Webshop.ModelLib.Models;


namespace Webshop.Logic.Products
{
    public class CreateProduct
    {
        private AppDbContext _context;
        public CreateProduct(AppDbContext context)
        {
            _context = context;
        }

        public void Run(int id, string name, string description)
        {
            _context.Products.Add(new Product()
            {
                Id = id,
                Name = name,
                Description = description
            });
        }
    }
}
