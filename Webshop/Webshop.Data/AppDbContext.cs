using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Webshop.Interface;
using System;
using System.Collections.Generic;
using System.Text;


namespace Webshop.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<StockDTO> Stock { get; set; }
        public DbSet<OrderProductDTO> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderProductDTO>().HasKey(a => new { a.ProductId, a.OrderId });
        }
    }
}
