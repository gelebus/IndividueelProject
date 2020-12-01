﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data.InterFaces;
using Webshop.ModelLib.Models;

namespace Webshop.Data
{
    public class ProductManager : IAdminProductFunctions, IUserProductFunctions
    {
        private AppDbContext _context;

        public ProductManager(AppDbContext context)
        {
            _context = context;
        }
        async Task IAdminProductFunctions.CreateProduct(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();
        }
        async Task<Product> IAdminProductFunctions.UpdateProduct(Product request)
        {
            var product = _context.Products.FirstOrDefault(a => a.Id == request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;    

            await _context.SaveChangesAsync();
            return product;
        }
        async Task IAdminProductFunctions.RemoveProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(a => a.Id == id);
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
        Product IAdminProductFunctions.GetProduct(int id)
        {
            Product product = _context.Products.Where(a => a.Id == id).Select(a => new Product
            {
                Id = a.Id,
                Value = a.Value,
                Name = a.Name,
                Description = a.Description
            }).FirstOrDefault();
            return product;
        }

        IEnumerable<Product> IAdminProductFunctions.GetProducts()
        {
            return GetProducts();
        }
        IEnumerable<Product> IUserProductFunctions.GetProducts()
        {
            return GetProducts();
        }

        IEnumerable<Product> GetProducts()
        {
            string connectionstring = "Server=mssql.fhict.local;Database=dbi407367_indiv2020;User Id=dbi407367_indiv2020;Password=r8u3u6d1;";
            string command = "SELECT * FROM [Products]";
            List<Product> products = new List<Product>();

            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                using(SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();

                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        products.Add(new Product()
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetDecimal(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3)
                        });
                    }
                }
            }
          /*IEnumerable<Product> products = _context.Products.ToList().Select(a => new Product
            {
                Id = a.Id,
                Description = a.Description,
                Value = a.Value,
                Name = a.Name
            });*/
            return products;
        }
    }
}
