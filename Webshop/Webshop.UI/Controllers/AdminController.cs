﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Logic.AdminProducts;
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
        public async Task<IActionResult> CreateProduct([FromBody] ProductViewModel productViewModel)
        {
            return Ok(await new CreateProduct(_context).Run(productViewModel));
        }

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(new GetProduct(_context).Run(id));
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(new GetProducts(_context).Run());
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            return Ok(await new RemoveProduct(_context).Run(id));
        }

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel productViewModel)
        {
            return Ok(await new UpdateProduct(_context).Run(productViewModel));
        }

    }
}
