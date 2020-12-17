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

            var orderdtos = new OrderFunctions(conString).GetOrders();

            foreach(var order in orderdtos)
            {
                orders.Add(order);
            }
            foreach(var order in orders)
            {
                string[] Items = order.OrderReference.Split(' ');
                order.products = getCartProducts(Items);
            }

            Orders = orders;
        }

        private List<CartProductViewModel> getCartProducts(string[]Items)
        {
            List<CartProductViewModel> cart = new List<CartProductViewModel>();
            foreach(var item in Items)
            {
                if (item != "")
                {
                    string[] values = item.Split('+');
                    int[] numbers = Array.ConvertAll(values, int.Parse);
                    cart.Add(new CartProductViewModel()
                    {
                        StockId = numbers[0],
                        Quantity = numbers[1]
                    });
                }
            }
            foreach(var product in cart)
            {
                var p = new ProductFunctions(conString).RunGetProductByStockId(product.StockId);
                product.Name = p.Name;
                product.Value = $"€{p.Value.ToString("N2")}";
            }

            return cart;
        }
    }
}
