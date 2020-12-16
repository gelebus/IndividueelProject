using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Logic.Products;
using Webshop.Logic.Stock;
using Webshop.Logic.ViewModels;

namespace Webshop.UnitTests
{
    [TestClass]
    public class StockTests
    {
        private readonly string ConnectionString = "Server=mssql.fhict.local;Database=dbi407367_indiv2020;User Id=dbi407367_indiv2020;Password=r8u3u6d1;";
        private static AdminProductViewModel ProductInDb;
        private static StockViewModel Stock;

        [TestMethod]
        public void TestA_CreateStock()
        {
            CreateTestProduct();
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
            Assert.AreEqual(stock.Description, "UnitTestStockDescription");
            Assert.AreEqual(stock.ProductId, ProductInDb.Id);
            Assert.AreEqual(stock.Quantity, 2);
        }
        [TestMethod]
        public void TestB_GetStock()
        {
            IEnumerable<AdminProductViewModel> stock = new StockFunctions(ConnectionString).RunGetStock();
            foreach (var s in stock)
            {
                Assert.IsNotNull(s.Stock);
            }
        }
        [TestMethod]
        public void TestC_UpdateStock()
        {
            List<StockViewModel> updatedStock = new List<StockViewModel>();
            updatedStock.Add(Stock);
            updatedStock[0].Quantity = 3;
            new StockFunctions(ConnectionString).RunUpdateStock(updatedStock);
            IEnumerable<AdminProductViewModel> stock = new StockFunctions(ConnectionString).RunGetStock();
            foreach (var product in stock)
            {
                foreach (var s in product.Stock)
                {
                    if (s.Id == Stock.Id)
                    {
                        Assert.AreEqual(updatedStock[0].Quantity, s.Quantity);
                    }
                }
            }
        }
        [TestMethod]
        public void TestD_RemoveStock()
        {
            Assert.IsTrue(new StockFunctions(ConnectionString).RunRemoveStock(Stock.Id));
            DeleteTestProduct();
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
