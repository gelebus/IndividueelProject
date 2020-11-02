using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webshop.Data;
using Webshop.Logic.ViewModels;

namespace Webshop.Logic.AdminProducts
{
    public class GetProducts
    {
        private AppDbContext _context;

        public GetProducts(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AdminProductViewModel> Run()
        {
            IEnumerable<AdminProductViewModel> products = _context.Products.ToList().Select(a => new AdminProductViewModel
            {
                Id = a.Id,
                Description = a.Description,
                Value = a.Value,
                Name = a.Name
            });
            return products;
        }
    }
}
