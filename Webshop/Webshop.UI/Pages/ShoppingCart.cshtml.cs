using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Logic;

namespace Webshop.UI.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly string conString;

        public ShoppingCartModel(AppDbContext context)
        {
            conString = context.Database.GetDbConnection().ConnectionString;
        }

        public IEnumerable<CartProductViewModel> Cart { get; set; }
        public IActionResult OnGet()
        {
            Cart = new ShoppingCart(HttpContext.Session,conString).GetShoppingCart();
            return Page();
        }
    }
}
