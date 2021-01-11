using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Logic.Products;
using Webshop.Logic.ViewModels;
using Webshop.Interface;

namespace Webshop.UnitTests
{
    [TestClass]
    public class ProductFunctionsTests
    {
        private readonly ProductFunctions productFunctions;
        private readonly Mock<IAdminProductFunctions> adminProductFunctions = new Mock<IAdminProductFunctions>();
        private readonly Mock<IUserProductFunctions> userProductFunctions = new Mock<IUserProductFunctions>();
        private readonly Mock<IProduct> product = new Mock<IProduct>();

        public ProductFunctionsTests()
        {
            productFunctions = new ProductFunctions(null, adminProductFunctions.Object, userProductFunctions.Object, product.Object);
        }

        [TestMethod]
        public void GetProductWithId()
        {
            //arrange
            int productId = 10;
            string productName = "testproduct";
            string productDesc = "testdesc";
            decimal productValue = 2;
            var productDTO = new ProductDTO()
            {
                Id = productId,
                Name = productName,
                Description = productDesc,
                Value = productValue
            };
            adminProductFunctions.Setup(x => x.GetProduct(productId)).Returns(productDTO);

            //act
            var product = productFunctions.RunGetProduct(productId);

            //assert
            Assert.AreEqual(productId, product.Id);
            Assert.AreEqual(productDesc, product.Description);
            Assert.AreEqual(productName, product.Name);
            Assert.AreEqual(productValue, product.Value);
        }
        [TestMethod]
        public void GetProductWithName()
        {
            //arrange
            int productId = 10;
            string productName = "testproduct";
            string productDesc = "testdesc";
            decimal productValue = 2;
            var productDTO = new ProductDTO()
            {
                Id = productId,
                Name = productName,
                Description = productDesc,
                Value = productValue,
                Stock = new List<StockDTO>()
            };
            userProductFunctions.Setup(x => x.GetProduct(productName)).Returns(productDTO);

            //act
            var product = productFunctions.RunGetUserProduct(productName);

            //assert
            Assert.AreEqual(productDesc, product.Description);
            Assert.AreEqual(productName, product.Name);
            Assert.AreEqual($"€{productValue.ToString("N2")}", product.Value);
        }

        [TestMethod]
        public void GetProducts()
        {
            //arrange
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            List<ProductDTO> checkList = new List<ProductDTO>();
            productDTOs.Add(new ProductDTO()
            {
                Id = 1,
                Name = "testProduct1",
                Description = "testdesc",
                Value = 3
            });
            productDTOs.Add(new ProductDTO()
            {
                Id = 2,
                Name = "testProduct2",
                Description = "testdesc2",
                Value = 2
            });
            adminProductFunctions.Setup(x => x.GetProducts()).Returns(productDTOs);

            //act
            IEnumerable<AdminProductViewModel>result = productFunctions.RunGetProducts();
            foreach(var p in result)
            {
                checkList.Add(new ProductDTO()
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Value = p.Value
                });
            }

            //assert
            Assert.AreEqual(productDTOs[0].Id, checkList[0].Id);
            Assert.AreEqual(productDTOs[0].Description, checkList[0].Description);
            Assert.AreEqual(productDTOs[0].Name, checkList[0].Name);
            Assert.AreEqual(productDTOs[0].Value, checkList[0].Value);

            Assert.AreEqual(productDTOs[1].Id, checkList[1].Id);
            Assert.AreEqual(productDTOs[1].Description, checkList[1].Description);
            Assert.AreEqual(productDTOs[1].Name, checkList[1].Name);
            Assert.AreEqual(productDTOs[1].Value, checkList[1].Value);
        }

        [TestMethod]
        public void CreateProduct()
        {
            //arrange
            int productId = 10;
            string productName = "testproduct";
            string productDesc = "testdesc";
            decimal productValue = 2;
            var productDTO = new ProductDTO()
            {
                Id = productId,
                Name = productName,
                Description = productDesc,
                Value = productValue
            };


            //act

            //assert
        }
    }
}
