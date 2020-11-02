using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using System.Linq;

namespace Webshop.Logic.AdminProducts
{
    public class RemoveProduct
    {
        private AppDbContext _context;
        public RemoveProduct(AppDbContext context)
        {
            _context = context;
        }

        public async Task Run(int id)
        {
            var product = _context.Products.FirstOrDefault(a => a.Id == id);
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}
