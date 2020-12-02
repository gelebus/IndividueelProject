using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webshop.ModelLib.Models;

namespace Webshop.Data.InterFaces
{
    public interface IAdminProductFunctions
    {
        Product CreateProduct(Product product);
        Product UpdateProduct(Product request);
        void RemoveProduct(int id);
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts();
    }
}
