using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Interface
{
    public interface IProduct
    {
        ProductDTO UpdateProduct(ProductDTO request);
    }
}
