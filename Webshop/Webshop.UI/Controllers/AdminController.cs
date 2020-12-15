using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Logic.Products;
using Webshop.Logic.Stock;
using Webshop.Logic.ViewModels;

namespace Webshop.UI.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly string ConString;

        public AdminController(AppDbContext context)
        {
            ConString = context.Database.GetDbConnection().ConnectionString;
        }

        [HttpPost("products")]
        public IActionResult CreateProduct([FromBody] ProductViewModel productViewModel)
        {
            return Ok(new ProductFunctions(ConString).RunCreateProduct(productViewModel));
        }

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(new ProductFunctions(ConString).RunGetProduct(id));
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(new ProductFunctions(ConString).RunGetProducts());
        }

        [HttpDelete("products/{id}")]
        public IActionResult RemoveProduct(int id)
        {
            return Ok(new ProductFunctions(ConString).RunRemoveProduct(id));
        }

        [HttpPut("products")]
        public IActionResult UpdateProduct([FromBody] AdminProductViewModel productViewModel)
        {
            return Ok(new ProductFunctions(ConString).RunUpdateProduct(productViewModel));
        }




        [HttpPost("stocks")]
        public IActionResult CreateStock([FromBody] StockViewModel stockViewModel)
        {
            return Ok(new StockFunctions(ConString).RunCreateStock(stockViewModel));
        }

        [HttpGet("stocks")]
        public IActionResult GetStock()
        {
            return Ok(new StockFunctions(ConString).RunGetStock());
        }

        [HttpDelete("stocks/{id}")]
        public IActionResult RemoveStock(int id)
        {
            return Ok(new StockFunctions(ConString).RunRemoveStock(id));
        }

        [HttpPut("stocks")]
        public IActionResult UpdateStock([FromBody] IEnumerable<StockViewModel> stockViewModels)
        {
            return Ok(new StockFunctions(ConString).RunUpdateStock(stockViewModels));
        }
    }
}
