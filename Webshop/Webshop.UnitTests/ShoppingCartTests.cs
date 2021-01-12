using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Interface;
using Webshop.Logic;

namespace Webshop.UnitTests
{
    [TestClass]
    public class ShoppingCartTests
    {
        private readonly ShoppingCart Cart;
        private readonly Mock<IShoppingCart> ShoppingCart = new Mock<IShoppingCart>();
        private readonly Mock<ISession> session = new Mock<ISession>();
        private readonly Mock<IStockFunctions> stockFunctions = new Mock<IStockFunctions>();

        public ShoppingCartTests()
        {
            Cart = new ShoppingCart(session.Object, null, ShoppingCart.Object, stockFunctions.Object);
        }

        [TestMethod]
        public void AddToShoppingCart()
        {
            //arrange
            //gettingstock

            List<IStockFunctions.StockResponse> stockResponses = new List<IStockFunctions.StockResponse>();
            List<StockDTO> stockList = new List<StockDTO>();
            int productId = 4;
            string productDesc = "testProductDesc";
            string productName = "testProduct";

            int stockId = 1;
            int stockQuantity = 3;
            string stockDesc = "teststockdesc";

            stockList.Add(new StockDTO()
            {
                Id = stockId,
                Description = stockDesc,
                Quantity = stockQuantity,
                ProductId = productId
            });
            IStockFunctions.StockResponse stockResponse = new IStockFunctions.StockResponse()
            {
                Id = productId,
                Description = productDesc,
                Name = productName,
                Stock = stockList
            };

            stockResponses.Add(stockResponse);
            stockFunctions.Setup(x => x.GetStock()).Returns(stockResponses);

            //cart
            CartProductViewModel product = new CartProductViewModel()
            {
                Name = "testProduct",
                Quantity = 2,
                StockId = 3,
                Value = "4"
            };
            var value = new byte[0];
            session.Setup(x => x.TryGetValue(It.IsAny<string>(), out value));
            session.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()));

            //act
            Cart.AddToShoppingCart(product);

            //assert
            session.Verify(x => x.TryGetValue(It.IsAny<string>(), out value));
            session.Verify(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()));
        }
        [TestMethod]
        public void GetShoppingCart()
        {
            //arrange
            var value = new byte[0];
            session.Setup(x => x.TryGetValue(It.IsAny<string>(), out value)).Returns(true);


            //act
            Cart.GetShoppingCart();

            //assert
            session.Verify(x => x.TryGetValue(It.IsAny<string>(), out value));
            
        }
    }
}
