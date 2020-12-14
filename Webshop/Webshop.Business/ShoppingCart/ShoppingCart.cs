using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Webshop.Interface;
using Webshop.Data.Managers;
using Webshop.Data;

namespace Webshop.Logic
{
    public class ShoppingCart
    {
        private ISession _session;
        private IShoppingCart IShoppingCart;

        public ShoppingCart(ISession session, AppDbContext context)
        {
            _session = session;
            IShoppingCart = new ShoppingCartManager(context);
        }

        public void AddToShoppingCart(CartProductViewModel product)
        {
            if(product.Quantity < 0)
            {
                product.Quantity = 0;
            }
            var stringObject = JsonConvert.SerializeObject(product);
            _session.SetString("shoppingCart", stringObject);
        }

        public CartProductViewModel GetShoppingCart()
        {
            var stringObject = _session.GetString("shoppingCart");
            var cartProduct = JsonConvert.DeserializeObject<CartProductViewModel>(stringObject);

            CartProductDTO response = IShoppingCart.GetCart(cartProduct.StockId);

            return new CartProductViewModel()
            {
                Name = response.Name,
                Quantity = cartProduct.Quantity,
                StockId = response.StockId,
                Value = response.Value
            };
        }
    }
}
