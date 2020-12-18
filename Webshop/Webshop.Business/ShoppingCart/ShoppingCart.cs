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
using Webshop.Logic.Products;
using Webshop.Logic.Stock;
using Webshop.Logic.ViewModels;

namespace Webshop.Logic
{
    public class ShoppingCart
    {
        private string _constring;
        private ISession _session;
        private IShoppingCart IShoppingCart;

        public ShoppingCart(ISession session, string conString)
        {
            _constring = conString;
            _session = session;
            IShoppingCart = Factory.Factory.CreateIShoppingCart(conString);
        }

        public void AddToShoppingCart(CartProductViewModel product)
        {
            product = AdditionChecks(product);

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

        public void ClearShoppingCart()
        {
            string stringObject = JsonConvert.SerializeObject(new List<CartProductViewModel>());
            _session.SetString("shoppingCart", stringObject);
        }

        private CartProductViewModel AdditionChecks(CartProductViewModel product)
        {
            IEnumerable<AdminProductViewModel> products = new StockFunctions(_constring).RunGetStock();
            foreach (var p in products)
            {
                foreach (var s in p.Stock)
                {
                    if (s.Id == product.StockId && product.Quantity > s.Quantity)
                    {
                        product.Quantity = s.Quantity;
                    }
                }
            }
            if (product.Quantity < 1)
            {
                product.Quantity = 1;
            }
            return product;
        }
    }
}
