using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Logic;
using Webshop.Logic.Products;
using Webshop.Logic.ViewModels;

namespace Webshop.UI.Pages
{
    public class ProductModel : PageModel
    {
        private readonly string ConString;

        public ProductModel(AppDbContext context)
        {
            ConString = context.Database.GetDbConnection().ConnectionString;
        }

        [BindProperty]
        public CartProductViewModel CartProduct { get; set; }
        
        public int StockCounter { get; set; }

        public ProductViewModel Product { get; set; }
        public IActionResult OnGet(string name)
        {
            StockCounter = 0;
            Product = new ProductFunctions(ConString).RunGetUserProduct(name.Replace("-", " "));
            if(Product == null)
            {
                return RedirectToPage("Index");
            }
            else
            {
                foreach(var s in Product.Stock)
                {
                    if(s.Quantity > 0)
                    {
                        StockCounter++;
                    }      
                }
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            new ShoppingCart(HttpContext.Session, ConString).AddToShoppingCart(CartProduct);
            

            return RedirectToPage("ShoppingCart");
        }


    }
}
