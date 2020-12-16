using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Webshop.Logic;
using Webshop.Logic.Products;
using Webshop.Logic.Stock;
using Webshop.Logic.ViewModels;

namespace Webshop.UnitTests
{
    [TestClass]
    public class ProductTests
    {
        private readonly string ConnectionString = "Server=mssql.fhict.local;Database=dbi407367_indiv2020;User Id=dbi407367_indiv2020;Password=r8u3u6d1;";
        private static AdminProductViewModel ProductInDb;
        private static AdminProductViewModel ProductInDbWithStock;

        //These tests are in order (this way i only need to add 2 products and remove those products of course)
        [TestMethod]
        public void TestA_CreateProduct()
        {
            ProductInDb = new AdminProductViewModel();
            AdminProductViewModel addedProduct = new ProductFunctions(ConnectionString).RunCreateProduct(new ProductViewModel()
            {
                Name = "UnitTestName",
                Description = "UnitTestDescription",
                Value = "10.01"
            });
            ProductInDb = new ProductFunctions(ConnectionString).RunGetProduct(addedProduct.Id);

            Assert.AreEqual(addedProduct.Name, ProductInDb.Name);
            Assert.AreEqual(addedProduct.Description, ProductInDb.Description);
            Assert.AreEqual(addedProduct.Value, ProductInDb.Value);
        }
        [TestMethod]
        public void TestA2_CreateProductWithStock()
        {
            ProductInDbWithStock = new AdminProductViewModel();
            
            
            AdminProductViewModel addedProduct = new ProductFunctions(ConnectionString).RunCreateProduct(new ProductViewModel()
            {
                Name = "UnitTestName2",
                Description = "UnitTestDescription",
                Value = "10.01"
            });
            new StockFunctions(ConnectionString).RunCreateStock(new StockViewModel
            {
                Description = "unittestdesc",
                ProductId = addedProduct.Id,
                Quantity = 4
            });
            ProductInDbWithStock = new ProductFunctions(ConnectionString).RunGetProduct(addedProduct.Id);

            Assert.AreEqual(addedProduct.Name, ProductInDbWithStock.Name);
            Assert.AreEqual(addedProduct.Description, ProductInDbWithStock.Description);
            Assert.AreEqual(addedProduct.Value, ProductInDbWithStock.Value);
        }

        [TestMethod]
        public void TestB1_GetProduct()
        {
            AdminProductViewModel product = new ProductFunctions(ConnectionString).RunGetProduct(ProductInDb.Id);
            ProductViewModel userProduct = new ProductFunctions(ConnectionString).RunGetUserProduct(ProductInDb.Name);

            Assert.AreEqual(product.Id, ProductInDb.Id);
            Assert.AreEqual(product.Description, ProductInDb.Description);
            Assert.AreEqual(product.Name, ProductInDb.Name);
            Assert.AreEqual(userProduct.Description, ProductInDb.Description);
            Assert.AreEqual(userProduct.Name, ProductInDb.Name);
        }
        [TestMethod]
        public void TestB2_GetProductWithStock()
        {
            AdminProductViewModel product = new ProductFunctions(ConnectionString).RunGetProduct(ProductInDbWithStock.Id);
            ProductViewModel userProduct = new ProductFunctions(ConnectionString).RunGetUserProduct(ProductInDbWithStock.Name);

            Assert.AreEqual(product.Id, ProductInDbWithStock.Id);
            Assert.AreEqual(product.Description, ProductInDbWithStock.Description);
            Assert.AreEqual(product.Name, ProductInDbWithStock.Name);
            Assert.AreEqual(userProduct.Description, ProductInDbWithStock.Description);
            Assert.AreEqual(userProduct.Name, ProductInDbWithStock.Name);
        }

        [TestMethod]
        public void TestC1_UpdateProduct()
        {
            AdminProductViewModel updatedProduct = new AdminProductViewModel() 
            { 
                Name = "ChangedUnitTestName",
                Description = ProductInDb.Description,
                Id = ProductInDb.Id,
                Value = ProductInDb.Value
            };

            new ProductFunctions(ConnectionString).RunUpdateProduct(updatedProduct);

            AdminProductViewModel updatedProductInDb = new ProductFunctions(ConnectionString).RunGetProduct(updatedProduct.Id);

            Assert.AreEqual(updatedProduct.Name, updatedProductInDb.Name);
        }
        public void TestC2_UpdateProductWithStock()
        {
            AdminProductViewModel updatedProduct = new AdminProductViewModel()
            {
                Name = "ChangedUnitTestName",
                Description = ProductInDb.Description,
                Id = ProductInDb.Id,
                Value = ProductInDb.Value,
                Stock = ProductInDbWithStock.Stock
            };

            new ProductFunctions(ConnectionString).RunUpdateProduct(updatedProduct);

            AdminProductViewModel updatedProductInDb = new ProductFunctions(ConnectionString).RunGetProduct(updatedProduct.Id);

            Assert.AreEqual(updatedProduct.Name, updatedProductInDb.Name);
        }

        [TestMethod]
        public void TestD1_RemoveProduct()
        {
            bool removed = new ProductFunctions(ConnectionString).RunRemoveProduct(ProductInDb.Id);

            Assert.IsTrue(removed);
        }
        [TestMethod]
        public void TestD2_RemoveProductWithStock()
        {
            bool removed = new ProductFunctions(ConnectionString).RunRemoveProduct(ProductInDbWithStock.Id);

            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void GetProducts()
        {
            IEnumerable<AdminProductViewModel> products = new ProductFunctions(ConnectionString).RunGetProducts();
            IEnumerable<ProductViewModel> userProducts = new ProductFunctions(ConnectionString).RunGetUserProducts();
            
            Assert.IsNotNull(products);
            Assert.IsNotNull(userProducts);
        }
        
    }
}
