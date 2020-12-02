using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Logic.AdminProducts;
using Webshop.Logic.AdminStock;
using Webshop.Logic.ViewModels;

namespace Webshop.UI.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("products")]
        public IActionResult CreateProduct([FromBody] ProductViewModel productViewModel)
        {
            return Ok(new AdminProductFunctions(_context).RunCreateProduct(productViewModel));
        }

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(new AdminProductFunctions(_context).RunGetProduct(id));
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(new AdminProductFunctions(_context).RunGetProducts());
        }

        [HttpDelete("products/{id}")]
        public IActionResult RemoveProduct(int id)
        {
            return Ok(new AdminProductFunctions(_context).RunRemoveProduct(id));
        }

        [HttpPut("products")]
        public IActionResult UpdateProduct([FromBody] AdminProductViewModel productViewModel)
        {
            return Ok(new AdminProductFunctions(_context).RunUpdateProduct(productViewModel));
        }




        [HttpPost("stocks")]
        public async Task<IActionResult> CreateStock([FromBody] StockViewModel stockViewModel)
        {
            return Ok(await new AdminStockFunctions(_context).RunCreateStock(stockViewModel));
        }

        [HttpGet("stocks")]
        public IActionResult GetStock()
        {
            return Ok(new AdminStockFunctions(_context).RunGetStock());
        }

        [HttpDelete("stocks/{id}")]
        public async Task<IActionResult> RemoveStock(int id)
        {
            return Ok(await new AdminStockFunctions(_context).RunRemoveStock(id));
        }

        [HttpPut("stocks")]
        public async Task<IActionResult> UpdateStock([FromBody] IEnumerable<StockViewModel> stockViewModel)
        {
            return Ok(await new AdminStockFunctions(_context).RunUpdateStock(stockViewModel));
        }



    }
}
