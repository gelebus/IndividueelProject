using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Logic.Products.ViewModels;
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

        public async Task Run(ProductViewModel productViewModel)
        {
            productViewModel.Value = productViewModel.Value.Replace('.', ',');
            _context.Products.Add(new Product()
            {
                Value = Convert.ToDecimal(productViewModel.Value),
                Name = productViewModel.Name,
                Description = productViewModel.Description
            });

            await _context.SaveChangesAsync();
        }
    }
}
