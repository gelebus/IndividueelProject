using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Logic.ViewModels;
using Webshop.ModelLib.Models;

namespace Webshop.Logic.AdminProducts
{
    public class UpdateProduct
    {
        private AppDbContext _context;
        public UpdateProduct(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminProductViewModel> Run(ProductViewModel productViewModel)
        {

            await _context.SaveChangesAsync();
            return new AdminProductViewModel();
        }
    }
}
