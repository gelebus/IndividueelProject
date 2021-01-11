using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Logic.Products;
using Webshop.Logic.ViewModels;

namespace Webshop.UI.Pages
{
    public class IndexModel : PageModel
    {
        private string ConString;

        public IndexModel(AppDbContext context)
        {
            ConString = context.Database.GetDbConnection().ConnectionString;
        }
        [BindProperty]
        public IEnumerable<ProductViewModel> Products { get; set; }
       
        public void OnGet()
        {
            Products = new ProductFunctions(ConString, null, null, null).RunGetUserProducts();
        }
    }
}
