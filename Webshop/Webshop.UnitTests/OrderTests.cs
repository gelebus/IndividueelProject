using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Logic.Order;
using Webshop.Logic.Products;
using Webshop.Logic.Stock;
using Webshop.Logic.ViewModels;

namespace Webshop.UnitTests
{
    [TestClass]
    public class OrderTests
    {
        private readonly string ConnectionString = "Server=mssql.fhict.local;Database=dbi407367_indiv2020;User Id=dbi407367_indiv2020;Password=r8u3u6d1;";
        private static AdminProductViewModel ProductInDb;
        private static StockViewModel Stock;
        private static OrderViewModel Order;

        [TestMethod]
        public void GetOrders()
        {
            IEnumerable<OrderViewModel> orders = new OrderFunctions(ConnectionString).GetOrders();
            Assert.IsNotNull(orders);
        }
        [TestMethod]
        public void TestA_CreateOrder()
        {
            Order = new OrderViewModel();
            CreateTestProduct();
            AddStockToTestProduct();
            OrderViewModel order = new OrderViewModel() 
            { 
                Adress = "test@gmail.com",
                City = "testCity",
                OrderReference = $"{Stock.Id}+{1} ",
                Postcode = "3642wg"
            };
            
            OrderViewModel result = new OrderFunctions(ConnectionString).CreateOrder(order);

            Order = result;

            Assert.AreEqual(order.Adress, Order.Adress);
            Assert.AreEqual(order.City, Order.City);
            Assert.AreEqual(order.Postcode, Order.Postcode);
            Assert.AreEqual(order.OrderReference, Order.OrderReference);
        }
        [TestMethod]
        public void TestB_RemoveOrder()
        {
            bool removed = new OrderFunctions(ConnectionString).RemoveOrder(Order.Id);
            Assert.IsTrue(removed);
            DeleteTestProduct();
        }

        private void AddStockToTestProduct()
        {
            StockViewModel stock = new StockFunctions(ConnectionString).RunCreateStock(new StockViewModel()
            {
                Description = "UnitTestStockDescription",
                ProductId = ProductInDb.Id,
                Quantity = 2
            });
            Stock = new StockViewModel()
            {
                Id = stock.Id,
                Description = stock.Description,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity
            };
        }
        private void CreateTestProduct()
        {
            ProductInDb = new AdminProductViewModel();
            AdminProductViewModel addedProduct = new ProductFunctions(ConnectionString).RunCreateProduct(new ProductViewModel()
            {
                Name = "UnitTestName",
                Description = "UnitTestDescription",
                Value = "10.01"
            });
            ProductInDb = new ProductFunctions(ConnectionString).RunGetProduct(addedProduct.Id);
        }

        private void DeleteTestProduct()
        {
            new ProductFunctions(ConnectionString).RunRemoveProduct(ProductInDb.Id);
        }
    }
}
