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
        public List<CartProductViewModel> Cart { get; set; }
        public IActionResult OnGet()
        {
            List<CartProductViewModel>cart = new List<CartProductViewModel>();
            IEnumerable<CartProductViewModel> cartlist = new ShoppingCart(HttpContext.Session,conString, null, null).GetShoppingCart();
            foreach(var cartproduct in cartlist)
            {
                cart.Add(cartproduct);
            }
            Cart = cart;
            return Page();
        }
    }
}
