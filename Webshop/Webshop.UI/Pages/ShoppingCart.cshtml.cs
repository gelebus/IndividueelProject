using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Data;
using Webshop.Logic;

namespace Webshop.UI.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private AppDbContext _context;

        public ShoppingCartModel(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CartProductViewModel> Cart { get; set; }
        public IActionResult OnGet()
        {
            Cart = new ShoppingCart(HttpContext.Session,_context).GetShoppingCart();
            return Page();
        }
    }
}
