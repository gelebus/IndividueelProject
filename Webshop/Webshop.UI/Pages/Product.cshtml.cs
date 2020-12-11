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
    public class ProductModel : PageModel
    {
        private AppDbContext _context;

        public ProductModel(AppDbContext context)
        {
            _context = context;
        }
        public ProductViewModel Product { get; set; }
        public IActionResult OnGet(string name)
        {
            Product = new ProductFunctions(_context).RunGetUserProduct(name.Replace("-", " "));
            if(Product == null)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }


    }
}
