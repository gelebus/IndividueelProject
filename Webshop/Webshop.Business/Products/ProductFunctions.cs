using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Interface;
using Webshop.Logic.ViewModels;
using Webshop.Factory;


namespace Webshop.Logic.Products
{
    public class ProductFunctions
    {
        private readonly string ConString;
        private IAdminProductFunctions iAdminProductFunctions;
        private IUserProductFunctions iUserProductFunctions;
        private IProduct iProduct;

        public ProductFunctions(string conString, IAdminProductFunctions testAdminProductFunctions, IUserProductFunctions testUserProductFunctions, IProduct testIProduct)
        {
            ConString = conString;
            if(testAdminProductFunctions != null || testUserProductFunctions != null || testIProduct != null)
            {
                iAdminProductFunctions = testAdminProductFunctions;
                iUserProductFunctions = testUserProductFunctions;
                iProduct = testIProduct;
            }
            else
            {
                iAdminProductFunctions = Factory.Factory.CreateIAdminProductFunctions(ConString);
                iUserProductFunctions = Factory.Factory.CreateIUserProductFunctions(ConString);
                iProduct = Factory.Factory.CreateIProduct(conString);
            }
        }

        public AdminProductViewModel RunCreateProduct(ProductViewModel productViewModel)
        {
            productViewModel.Value = productViewModel.Value.Replace('.', ',');
            ProductDTO product = new ProductDTO()
            {
                Value = Convert.ToDecimal(productViewModel.Value),
                Name = productViewModel.Name,
                Description = productViewModel.Description
            };
            product = iAdminProductFunctions.CreateProduct(product);

            return new AdminProductViewModel()
            {
                Id = product.Id,
                Value = product.Value,
                Name = product.Name,
                Description = product.Description
            };
        }
        public AdminProductViewModel RunGetProduct(int id)
        {
            ProductDTO product = iAdminProductFunctions.GetProduct(id);
            return new AdminProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }
        public AdminProductViewModel RunGetProductByStockId(int stockId)
        {
            ProductDTO product = iAdminProductFunctions.GetProductByStockId(stockId);
            return new AdminProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        public ProductViewModel RunGetUserProduct(string name)
        {
            ProductDTO product = iUserProductFunctions.GetProduct(name);
            if(product == null)
            {
                return null;
            }
            bool inStock = false;
            if(product.Stock.Count > 0)
            {
                foreach(var s in product.Stock)
                {
                    if(s.InStock)
                    {
                        inStock = true;
                    }
                }
            }
            return new ProductViewModel()
            {
                Name = product.Name,
                Description = product.Description,
                Value = $"€{product.Value.ToString("N2")}",
                InStock = inStock,
                Stock = product.Stock.Select(a => new StockViewModel 
                {
                    Id = a.Id,
                    ProductId = a.ProductId,
                    Quantity = a.Quantity,
                    Description = a.Description
                })
            };
        }
        public IEnumerable<AdminProductViewModel> RunGetProducts()
        {
            List<AdminProductViewModel> productVms = new List<AdminProductViewModel>();
            IEnumerable<ProductDTO> products = iAdminProductFunctions.GetProducts();
            
            foreach (ProductDTO product in products)
            {
                AdminProductViewModel adminProductViewModel = new AdminProductViewModel()
                {
                    Id = product.Id,
                    Description = product.Description,
                    Value = product.Value,
                    Name = product.Name
                };
                productVms.Add(adminProductViewModel);
            }
            return productVms;
        }
        public IEnumerable<ProductViewModel> RunGetUserProducts()
        {
            List<ProductViewModel> productVms = new List<ProductViewModel>();
            IEnumerable<ProductDTO> products = iUserProductFunctions.GetProducts();

            foreach (ProductDTO product in products)
            {
                ProductViewModel ProductViewModel = new ProductViewModel()
                {
                    Description = product.Description,
                    Value = $"€{product.Value.ToString("N2")}",
                    Name = product.Name
                };
                productVms.Add(ProductViewModel);
            }
            return productVms;
        }
        public bool RunRemoveProduct(int id)
        {
            iAdminProductFunctions.RemoveProduct(id);
            return true;
        }
        public AdminProductViewModel RunUpdateProduct(AdminProductViewModel productViewModel)
        {
            ProductDTO response = new Product(productViewModel, iProduct).RunUpdate();
            AdminProductViewModel responseVm = new AdminProductViewModel()
            {
                Id = response.Id,
                Description = response.Description,
                Value = response.Value,
                Name = response.Name
            };
            return responseVm;
        }
    }
}
