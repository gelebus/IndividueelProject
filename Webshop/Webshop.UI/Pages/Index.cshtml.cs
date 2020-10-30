using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Data;
using Webshop.Logic.Products;
using Webshop.Logic.Products.ViewModels;

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
        public ProductViewModel ProductVm { get; set; }
        [BindProperty]
        public IEnumerable<GetProductsViewModel> Products { get; set; }
       
        public void OnGet()
        {
            Products = new GetProducts(_context).Run();
        }
        public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_context).Run(ProductVm);
            return RedirectToPage("Index");
        }
    }
}
