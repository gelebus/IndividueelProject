using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Logic.ViewModels;
using Webshop.ModelLib.Models;


namespace Webshop.Logic.AdminProducts
{
    public class CreateProduct
    {
        private AppDbContext _context;
        public CreateProduct(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminProductViewModel> Run(ProductViewModel productViewModel)
        {
            productViewModel.Value = productViewModel.Value.Replace('.', ',');

            Product product = new Product()
            {
                Value = Convert.ToDecimal(productViewModel.Value),
                Name = productViewModel.Name,
                Description = productViewModel.Description
            };
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return new AdminProductViewModel()
            {
                Id = product.Id,
                Value = product.Value,
                Name = product.Name,
                Description = product.Description
            };
        }
    }
}
