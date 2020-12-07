using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webshop.Data;
using Webshop.Interface;
using Webshop.Logic.ViewModels;


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
    }
}
