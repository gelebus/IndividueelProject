using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Interface;
using Webshop.Logic.ViewModels;

namespace Webshop.Logic.Order
{
    public class OrderFunctions
    {
        IOrderFunctions iorderFunctions;
        public OrderFunctions(string Constring)
        {
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

    }
}
