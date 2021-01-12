using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Webshop.Interface;
using Webshop.Logic.Products;
using Webshop.Logic.ViewModels;

namespace Webshop.UnitTests
{
    [TestClass]
    public class ProductTests
    {
        private readonly ProductFunctions productFunctions;
        private readonly Mock<IAdminProductFunctions> adminProductFunctions = new Mock<IAdminProductFunctions>();
        private readonly Mock<IUserProductFunctions> userProductFunctions = new Mock<IUserProductFunctions>();
        private readonly Mock<IProduct> product = new Mock<IProduct>();

        public ProductTests()
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
            List<StockDTO> Stock = new List<StockDTO>();
            Stock.Add(new StockDTO() { InStock = true });
            var productDTO = new ProductDTO()
            {
                Id = productId,
                Name = productName,
                Description = productDesc,
                Value = productValue,
                Stock = Stock
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
        public void GetProductWithNameReturnsNullIfProductIsNull()
        {
            //arrange
            userProductFunctions.Setup(x => x.GetProduct(It.IsAny<string>())).Returns((ProductDTO)null);

            //act
            var product = productFunctions.RunGetUserProduct("Unknown");

            //assert
            Assert.IsNull(product);
        }
        

        [TestMethod]
        public void GetProductWithStockId()
        {
            //arrange
            int productId = 10;
            int stockId = 1;
            string productName = "testproduct";
            string productDesc = "testdesc";
            decimal productValue = 2;
            List<StockDTO> Stock = new List<StockDTO>();
            Stock.Add(new StockDTO() 
            {
                Id = stockId
            });

            var productDTO = new ProductDTO()
            {
                Id = productId,
                Name = productName,
                Description = productDesc,
                Value = productValue,
                Stock = Stock
            };
            adminProductFunctions.Setup(x => x.GetProductByStockId(stockId)).Returns(productDTO);

            //act
            AdminProductViewModel product = productFunctions.RunGetProductByStockId(stockId);

            //assert
            Assert.AreEqual(productId, product.Id);
            Assert.AreEqual(productDesc, product.Description);
            Assert.AreEqual(productName, product.Name);
            Assert.AreEqual(productValue, product.Value);

        }

        [TestMethod]
        public void GetUserProducts()
        {
            //arrange
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            List<ProductDTO> checkList = new List<ProductDTO>();
            productDTOs.Add(new ProductDTO()
            {
                Id = 1,
                Name = "testProduct1",
                Description = "testdesc"
            });
            productDTOs.Add(new ProductDTO()
            {
                Id = 2,
                Name = "testProduct2",
                Description = "testdesc2",
            });
            userProductFunctions.Setup(x => x.GetProducts()).Returns(productDTOs);

            //act
            IEnumerable<ProductViewModel>result = productFunctions.RunGetUserProducts();
            foreach(var p in result)
            {
                checkList.Add(new ProductDTO()
                {
                    Description = p.Description,
                    Name = p.Name
                });
            }

            //assert
            Assert.AreEqual(productDTOs[0].Description, checkList[0].Description);
            Assert.AreEqual(productDTOs[0].Name, checkList[0].Name);

            Assert.AreEqual(productDTOs[1].Description, checkList[1].Description);
            Assert.AreEqual(productDTOs[1].Name, checkList[1].Name);
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
            IEnumerable<AdminProductViewModel> result = productFunctions.RunGetProducts();
            foreach (var p in result)
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
            ProductDTO productDTO = new ProductDTO()
            {
                Id = productId,
                Name = productName,
                Description = productDesc,
                Value = productValue
            };

            ProductViewModel productvm = new ProductViewModel()
            {
                Name = productName,
                Description = productDesc,
                Value = productValue.ToString()
            };

            adminProductFunctions.Setup(x => x.CreateProduct(It.IsAny<ProductDTO>())).Returns(productDTO);
            
            //act
            var product = productFunctions.RunCreateProduct(productvm);

            //assert
            Assert.AreEqual(productId, product.Id);
            Assert.AreEqual(productDesc, product.Description);
            Assert.AreEqual(productValue, product.Value);
            Assert.AreEqual(productName, product.Name);
        }

        [TestMethod]
        public void RemoveProduct()
        {
            //arrange
            int productId = 10;

            adminProductFunctions.Setup(x => x.RemoveProduct(It.IsAny<int>()));

            // act
            productFunctions.RunRemoveProduct(productId);

            // assert
            adminProductFunctions.Verify(x => x.RemoveProduct(productId));
        }
        [TestMethod]
        public void UpdateProductWithoutStock()
        {
            //arange
            int productId = 10;
            string productName = "testproduct";
            string productDesc = "testdesc";
            decimal productValue = 2;
            AdminProductViewModel productvm = new AdminProductViewModel()
            {
                Id = productId,
                Name = productName,
                Description = productDesc,
                Value = productValue
            };
            product.Setup(x => x.UpdateProduct(It.IsAny<ProductDTO>())).Returns(new ProductDTO()
            {
                Id = productvm.Id,
                Name = productvm.Name,
                Value = productvm.Value,
                Description = productvm.Description
            });

            //act
            productFunctions.RunUpdateProduct(productvm);

            //assert
            product.Verify(x => x.UpdateProduct(It.IsAny<ProductDTO>()));
        }
        [TestMethod]
        public void UpdateProductWithStock()
        {
            //arange
            List<StockViewModel> stockList = new List<StockViewModel>();

            int productId = 10;
            string productName = "testproduct";
            string productDesc = "testdesc";
            decimal productValue = 2;

            int stockId = 3;
            int stockQuantity = 4;
            string stockDesc = "testStockDesc";

            stockList.Add(new StockViewModel()
            {
                Id = stockId,
                Description = stockDesc,
                Quantity = stockQuantity,
                ProductId = productId
            });
            AdminProductViewModel productvm = new AdminProductViewModel()
            {
                Id = productId,
                Name = productName,
                Description = productDesc,
                Value = productValue,
                Stock = stockList
            };
            product.Setup(x => x.UpdateProduct(It.IsAny<ProductDTO>())).Returns(new ProductDTO()
            {
                Id = productvm.Id,
                Name = productvm.Name,
                Value = productvm.Value,
                Description = productvm.Description
            });

            //act
            productFunctions.RunUpdateProduct(productvm);

            //assert
            product.Verify(x => x.UpdateProduct(It.IsAny<ProductDTO>()));
        }
    }
}
