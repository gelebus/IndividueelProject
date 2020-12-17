using System;
using Webshop.Data;
using Webshop.Data.Managers;
using Webshop.Interface;

namespace Webshop.Factory
{
    public class Factory
    {
        public static IProduct CreateIProduct(string con)
        { 
             return new ProductManager(con);
        }
        public static IAdminProductFunctions CreateIAdminProductFunctions(string con)
        {
            return new ProductManager(con);
        }
        public static IUserProductFunctions CreateIUserProductFunctions(string con)
        {
            return new ProductManager(con);
        }
        public static IStock CreateIStock(string con)
        {
            return new StockManager(con);
        }
        public static IStockFunctions CreateIStockFunctions(string con)
        {
            return new StockManager(con);
        }
        public static IShoppingCart CreateIShoppingCart(string con)
        {
            return new ShoppingCartManager(con);
        }

        public static IOrderFunctions CreateIOrderFunctions(string con)
        {
            return new OrderManager(con);
        }
    }
}
