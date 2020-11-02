using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Logic.ViewModels;
using System.Linq;
using Webshop.ModelLib.Models;

namespace Webshop.Logic.AdminProducts
{
    public class GetProduct
    {
        private AppDbContext _context;
        public GetProduct(AppDbContext context)
        {
            _context = context;
        }

        public AdminProductViewModel Run(int id)
        {
            AdminProductViewModel product = _context.Products.Where(a => a.Id == id).Select(a => new AdminProductViewModel
            {
                Id = a.Id,
                Value = a.Value,
                Name = a.Name,
                Description = a.Description
            }).FirstOrDefault();
            return product;
        }
    }
}
