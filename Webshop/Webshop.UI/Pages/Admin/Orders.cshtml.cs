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
using Webshop.Logic.Products;
using Webshop.Logic.ViewModels;

namespace Webshop.UI.Pages.Admin
{
    public class OrdersModel : PageModel
    {
        private readonly string conString;

        public OrdersModel(AppDbContext context)
        {
            conString = context.Database.GetDbConnection().ConnectionString;
        }

        public IEnumerable<OrderViewModel> Orders;
        
        public void OnGet()
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();

            var orderdtos = new OrderFunctions(conString,null,null,null,null).GetOrders();

            foreach(var order in orderdtos)
            {
                orders.Add(order);
            }
            foreach(var order in orders)
            {
                order.products = new OrderFunctions(conString,null,null,null,null).GetCartProductsFromOrderRef(order.OrderReference);
            }
            Orders = orders;
        }
    }
}
