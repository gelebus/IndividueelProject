using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Logic;
using Webshop.Logic.Order;
using Webshop.Logic.ViewModels;

namespace Webshop.UI.Pages
{
    public class OrderModel : PageModel
    {
        private readonly string Connectionstring;
        public OrderModel(AppDbContext context)
        {
            Connectionstring = context.Database.GetDbConnection().ConnectionString;
        }
        [BindProperty]
        public OrderViewModel Order { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            IEnumerable<CartProductViewModel> cart = new ShoppingCart(HttpContext.Session, Connectionstring, null, null).GetShoppingCart();
            string orderRef = "";
            foreach(var s in cart)
            {
                orderRef += $"{s.StockId}+{s.Quantity} ";
            }
            new OrderFunctions(Connectionstring, null,null,null,null).CreateOrder(new OrderViewModel()
            {
                OrderReference = orderRef,
                Adress = Order.Adress,
                City = Order.City,
                Postcode = Order.Postcode
            });
            new ShoppingCart(HttpContext.Session, Connectionstring, null, null).ClearShoppingCart();
            return RedirectToPage("OrderPlaced");
        }
    }
}
