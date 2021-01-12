using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Interface;
using Webshop.Logic.Order;
using Webshop.Logic.ViewModels;

namespace Webshop.UnitTests
{
    [TestClass]
    public class OrderTests
    {
        private readonly OrderFunctions orderFunctions;
        private readonly Mock<IOrderFunctions> iOrderFunctions = new Mock<IOrderFunctions>();
        private readonly Mock<IAdminProductFunctions> iProductFunctions = new Mock<IAdminProductFunctions>();
        private readonly Mock<IStockFunctions> iStockFunctions = new Mock<IStockFunctions>();
        private readonly Mock<IStock> iStock = new Mock<IStock>();
        public OrderTests()
        {
            orderFunctions = new OrderFunctions(null, iOrderFunctions.Object, iProductFunctions.Object, iStockFunctions.Object, iStock.Object);
        }

        [TestMethod]
        public void CreateOrder()
        {
            //arrange
            List<IStockFunctions.StockResponse> stockResponses = new List<IStockFunctions.StockResponse>();
            List<StockDTO> stockList = new List<StockDTO>();
            int productId = 4;
            string productDesc = "testProductDesc";
            string productName = "testProduct";

            int stockId = 1;
            int stockQuantity = 3;
            string stockDesc = "teststockdesc";

            int OrderId = 4;

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
            
            OrderViewModel ordervm = new OrderViewModel()
            {
                Adress = "Heraut 45",
                City = "Oss",
                Postcode = "5346TF",
                OrderReference = "1+3 "
            };

            iOrderFunctions.Setup(x => x.CreateOrder(It.IsAny<OrderDTO>())).Returns(new OrderDTO() 
            { 
                Id = OrderId,
                Adress = ordervm.Adress,
                City = ordervm.City,
                Postcode = ordervm.Postcode,
                OrderReference = "1+3 "
            });

            iProductFunctions.Setup(x => x.GetProductByStockId(It.IsAny<int>())).Returns(new ProductDTO()
            {
                Id = 3,
                Description = "productDesc",
                Name = "product",
                Value = 2
            });
            iStock.Setup(x => x.UpdateStock(It.IsAny<StockDTO>()));

            iStockFunctions.Setup(x => x.GetStock()).Returns(stockResponses);

            //act
            var Order = orderFunctions.CreateOrder(ordervm);

            //assert
            Assert.AreEqual(OrderId, Order.Id);
            Assert.AreEqual(ordervm.Adress, Order.Adress);
            Assert.AreEqual(ordervm.City, Order.City);
            Assert.AreEqual(ordervm.Postcode, Order.Postcode);
        }

        [TestMethod]
        public void GetOrders()
        {
            //arrange
            int orderId = 4;
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            OrderViewModel ordervm = new OrderViewModel()
            {
                Adress = "Heraut 45",
                City = "Oss",
                Postcode = "5346TF",
                OrderReference = "1+3 "
            };
            orderDTOs.Add(new OrderDTO()
            {
                Id = orderId,
                Adress = ordervm.Adress,
                City = ordervm.City,
                Postcode = ordervm.Postcode,
                OrderReference = "1+3 "
            });
            iOrderFunctions.Setup(x => x.GetOrders()).Returns(orderDTOs);

            //act
            var orders = orderFunctions.GetOrders();

            //assert
            foreach(var o in orders)
            {
                Assert.AreEqual(orderId, o.Id);
                Assert.AreEqual(ordervm.Adress, o.Adress);
                Assert.AreEqual(ordervm.City, o.City);
                Assert.AreEqual(ordervm.Postcode, o.Postcode);
                Assert.AreEqual(ordervm.OrderReference, o.OrderReference);
            }
        }
    }
}
