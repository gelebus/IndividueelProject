using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Interface;
using Webshop.Logic.Stock;
using Webshop.Logic.ViewModels;

namespace Webshop.UnitTests
{
    [TestClass]
    public class StockTests
    {
        private readonly StockFunctions stockFunctions;
        private readonly Mock<IStock> iStock = new Mock<IStock>();
        private readonly Mock<IStockFunctions> iStockFunctions = new Mock<IStockFunctions>();

        public StockTests()
        {
            stockFunctions = new StockFunctions(null, iStock.Object, iStockFunctions.Object);
        }
        [TestMethod]
        public void CreateStock()
        {
            //arrange
            int productId = 3;
            int quantity = 2;
            string description = "testdesc";
            StockViewModel stockvm = new StockViewModel()
            {
                ProductId = productId,
                Description = description,
                Quantity = quantity
            };
            iStockFunctions.Setup(x => x.CreateStock(It.IsAny<StockDTO>())).Returns(new StockDTO()
            {
                Id = 3,
                ProductId = stockvm.ProductId,
                Description = stockvm.Description,
                Quantity = stockvm.Quantity
            });

            //act
            var stock = stockFunctions.RunCreateStock(stockvm);

            //assert
            Assert.AreEqual(3, stock.Id);
            Assert.AreEqual(stockvm.Description, stock.Description);
            Assert.AreEqual(stockvm.Quantity, stock.Quantity);
            Assert.AreEqual(stockvm.ProductId, stock.ProductId);
        }
        [TestMethod]
        public void RemoveStock()
        {
            //arrange
            int Id = 4;
            iStockFunctions.Setup(x => x.RemoveStock(It.IsAny<int>()));

            //act
            bool removed = stockFunctions.RunRemoveStock(Id);

            //assert
            iStockFunctions.Verify(x => x.RemoveStock(Id));
            Assert.IsTrue(removed);
        }
        [TestMethod]
        public void UpdateStock()
        {
            //arrange
            List<StockViewModel> stockvms = new List<StockViewModel>();
            stockvms.Add(new StockViewModel());
            iStock.Setup(x => x.UpdateStock(It.IsAny<StockDTO>()));

            //act
            stockFunctions.RunUpdateStock(stockvms);

            //assert
            iStock.Verify(x => x.UpdateStock(It.IsAny<StockDTO>()));
        }
        [TestMethod]
        public void GetStock()
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
            iStockFunctions.Setup(x => x.GetStock()).Returns(stockResponses);

            //act
            var products = stockFunctions.RunGetStock();

            //assert
            foreach(var p in products)
            {
                Assert.AreEqual(productId, p.Id);
                Assert.AreEqual(productName, p.Name);
                Assert.AreEqual(productDesc, p.Description);
                
                foreach(var s in p.Stock)
                {
                    Assert.AreEqual(stockId, s.Id);
                    Assert.AreEqual(stockDesc, s.Description);
                    Assert.AreEqual(stockQuantity, s.Quantity);
                    Assert.AreEqual(productId, s.ProductId);
                }
            }
        }
    }
}
