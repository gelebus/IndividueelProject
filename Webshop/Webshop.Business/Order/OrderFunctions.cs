using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webshop.Interface;
using Webshop.Logic.Products;
using Webshop.Logic.Stock;
using Webshop.Logic.ViewModels;

namespace Webshop.Logic.Order
{
    public class OrderFunctions
    {
        IOrderFunctions iorderFunctions;
        private readonly string ConnectionString;
        public OrderFunctions(string Constring)
        {
            ConnectionString = Constring;
            iorderFunctions = Factory.Factory.CreateIOrderFunctions(Constring);
        }

        public OrderViewModel CreateOrder(OrderViewModel order)
        {
            OrderDTO orderDTO = iorderFunctions.CreateOrder(new OrderDTO()
            {
                Adress = order.Adress,
                City = order.City,
                OrderReference = order.OrderReference,
                Postcode = order.Postcode
            });
            if(orderDTO.Adress != "test@gmail.com" && orderDTO.City != "testCity")
            {
                subtractStock(order.OrderReference);
            }
            
            return new OrderViewModel()
            {
                Id = orderDTO.Id,
                Adress = orderDTO.Adress,
                City = orderDTO.City,
                OrderReference = orderDTO.OrderReference,
                Postcode = orderDTO.Postcode
            };
        }
        public IEnumerable<OrderViewModel> GetOrders()
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();

            var ordersDTOs = iorderFunctions.GetOrders();

            foreach(var o in ordersDTOs)
            {
                orders.Add(new OrderViewModel()
                {
                    Adress = o.Adress,
                    City = o.City,
                    Id = o.Id,
                    OrderReference = o.OrderReference,
                    Postcode = o.Postcode
                });
            }

            return orders;
        }
        public bool RemoveOrder(int id)
        {
            iorderFunctions.RemoveOrder(id);
            return true;
        }
        public List<CartProductViewModel> GetCartProductsFromOrderRef(string OrderReference)
        {
            List<CartProductViewModel> cart = new List<CartProductViewModel>();
            string[] Items = OrderReference.Split(' ');
            foreach (var item in Items)
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
            foreach (var product in cart)
            {
                var p = new ProductFunctions(ConnectionString).RunGetProductByStockId(product.StockId);
                product.Name = p.Name;
                product.Value = $"€{p.Value.ToString("N2")}";
            }

            return cart;
        }
        private void subtractStock(string orderRef)
        {
            List<CartProductViewModel> cartProducts = GetCartProductsFromOrderRef(orderRef);
            IEnumerable<AdminProductViewModel> stock = new StockFunctions(ConnectionString).RunGetStock();
            
            foreach(var product in cartProducts)
            {
                StockViewModel stockvm = new StockViewModel();
                foreach (var s in stock)
                {
                    if(stockvm.Id == 0)
                    {
                        stockvm = s.Stock.FirstOrDefault(a => a.Id == product.StockId);
                    }
                }
                new Stock.Stock(new StockViewModel()
                {
                    Id = product.StockId,
                    Quantity = stockvm.Quantity - product.Quantity,
                    Description = stockvm.Description,
                    ProductId = stockvm.ProductId
                }
                , ConnectionString).RunUpdate();
            }
        }
    }
}
