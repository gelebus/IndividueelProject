using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Interface
{
    public interface IUserProductFunctions
    {
        IEnumerable<ProductDTO> GetProducts();
    }
}
