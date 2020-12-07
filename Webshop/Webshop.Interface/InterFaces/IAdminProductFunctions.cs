using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Interface
{
    public interface IAdminProductFunctions
    {
        ProductDTO CreateProduct(ProductDTO product);
        ProductDTO UpdateProduct(ProductDTO request);
        void RemoveProduct(int id);
        ProductDTO GetProduct(int id);
        IEnumerable<ProductDTO> GetProducts();
    }
}
