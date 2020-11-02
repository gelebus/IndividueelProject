using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Data;
using Webshop.Logic.Products;
using Webshop.Logic.ViewModels;

namespace Webshop.UI.Pages
{
    public class IndexModel : PageModel
    {
        private AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IEnumerable<ProductViewModel> Products { get; set; }
       
        public void OnGet()
        {
            Products = new GetProducts(_context).Run();
        }
    }
}
