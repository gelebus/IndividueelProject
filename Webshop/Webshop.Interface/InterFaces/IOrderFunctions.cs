using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Interface
{
    public interface IOrderFunctions
    {
        OrderDTO CreateOrder(OrderDTO order);
        IEnumerable<OrderDTO> GetOrders();
        void RemoveOrder(int id);
    }
}
