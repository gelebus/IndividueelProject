using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Webshop.Interface;
using Webshop.Data.Managers;
using Webshop.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Logic
{
    public class ShoppingCart
    {
        private ISession _session;
        private IShoppingCart IShoppingCart;

        public ShoppingCart(ISession session, string conString)
        {
            _session = session;
            IShoppingCart = Factory.Factory.CreateIShoppingCart(conString);
        }

        public void AddToShoppingCart(CartProductViewModel product)
        {
            if(product.Quantity < 0)
            {
                product.Quantity = 0;
            }

            var cartlist = new List<CartProductViewModel>();
            var stringObject = _session.GetString("shoppingCart");

            if(!string.IsNullOrEmpty(stringObject))
            {
                cartlist = JsonConvert.DeserializeObject<List<CartProductViewModel>>(stringObject);
            }

            if(cartlist.Any(a => a.StockId == product.StockId))
            {
                cartlist.Find(a => a.StockId == product.StockId).Quantity += product.Quantity;
            }
            else
            {
                cartlist.Add(product);
            }

            stringObject = JsonConvert.SerializeObject(cartlist);
            _session.SetString("shoppingCart", stringObject);
        }

        public IEnumerable<CartProductViewModel> GetShoppingCart()
        {
            List<CartProductViewModel> cartProductViewModels = new List<CartProductViewModel>();
            var stringObject = _session.GetString("shoppingCart");
            if(string.IsNullOrEmpty(stringObject))
            {
                return new List<CartProductViewModel>();
            }
            var cartList = JsonConvert.DeserializeObject<List<CartProductViewModel>>(stringObject);

            foreach(var cartItem in cartList)
            {
                CartProductDTO response = IShoppingCart.GetCart(cartItem.StockId);
                cartProductViewModels.Add(new CartProductViewModel()
                {
                    Name = response.Name,
                    Quantity = cartItem.Quantity,
                    StockId = response.StockId,
                    Value = response.Value
                });
            }

            
            return cartProductViewModels;
        }
    }
}
