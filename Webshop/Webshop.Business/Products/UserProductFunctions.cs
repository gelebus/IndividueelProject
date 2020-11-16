using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webshop.Data;
using Webshop.Data.InterFaces;
using Webshop.Logic.ViewModels;
using Webshop.ModelLib.Models;

namespace Webshop.Logic.Products
{
    public class UserProductFunctions
    {
        private IUserProductFunctions iUserProductFunctions;

        public UserProductFunctions(AppDbContext context)
        {
            iUserProductFunctions = new ProductManager(context);
        }
        public IEnumerable<ProductViewModel> RunGetProducts()
        {
            List<ProductViewModel> productVms = new List<ProductViewModel>();
            IEnumerable<Product> products = iUserProductFunctions.GetProducts();

            foreach (Product product in products)
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
    }
}
