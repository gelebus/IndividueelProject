using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Data.InterFaces;
using Webshop.Logic.ViewModels;
using Webshop.ModelLib.Models;

namespace Webshop.Logic.AdminProducts
{
    public class AdminProductFunctions
    {
        private IAdminProductFunctions iAdminProductFunctions;

        public AdminProductFunctions(AppDbContext context)
        {
            string connectionstring = context.Database.GetDbConnection().ConnectionString;
            iAdminProductFunctions = new ProductManager(context);
        }

        public AdminProductViewModel RunCreateProduct(ProductViewModel productViewModel)
        {
            productViewModel.Value = productViewModel.Value.Replace('.', ',');
            Product product = new Product()
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
            Product product = iAdminProductFunctions.GetProduct(id);
            return new AdminProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }
        public IEnumerable<AdminProductViewModel> RunGetProducts()
        {
            List<AdminProductViewModel> productVms = new List<AdminProductViewModel>();
            IEnumerable<Product> products = iAdminProductFunctions.GetProducts();
            
            foreach (Product product in products)
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
        public bool RunRemoveProduct(int id)
        {
            iAdminProductFunctions.RemoveProduct(id);
            return true;
        }
        public AdminProductViewModel RunUpdateProduct(AdminProductViewModel productViewModel)
        {
            Product request = new Product()
            {
                Id = productViewModel.Id,
                Description = productViewModel.Description,
                Value = productViewModel.Value,
                Name = productViewModel.Name
            };
            Product response = iAdminProductFunctions.UpdateProduct(request);
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
