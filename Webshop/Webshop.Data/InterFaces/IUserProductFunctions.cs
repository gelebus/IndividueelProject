using System;
using System.Collections.Generic;
using System.Text;
using Webshop.ModelLib.Models;

namespace Webshop.Data.InterFaces
{
    public interface IUserProductFunctions
    {
        IEnumerable<Product> GetProducts();
    }
}
