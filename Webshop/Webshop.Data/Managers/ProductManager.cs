using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Interface;
using Webshop.ModelLib.Models;

namespace Webshop.Data
{
    public class ProductManager : IAdminProductFunctions, IUserProductFunctions
    {
        
        private string connectionstring;

        public ProductManager(AppDbContext context)
        {
            connectionstring = context.Database.GetDbConnection().ConnectionString;
        }
        Product IAdminProductFunctions.CreateProduct(Product product)
        {
            Product Product = new Product();
            string command1 = "INSERT INTO Products (Value,Name,Description) VALUES(@ProductValue,@ProductName,@ProductDesc)";
            string command2 = "SELECT * FROM [Products] WHERE Id IN(SELECT Max(Id) FROM [Products])";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                sqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand(command1, sqlconnection))
                {
                    cmd.Parameters.Add("@ProductValue", System.Data.SqlDbType.Decimal).Value = product.Value;
                    cmd.Parameters.Add("@ProductName", System.Data.SqlDbType.NVarChar).Value = product.Name;
                    cmd.Parameters.Add("@ProductDesc", System.Data.SqlDbType.NVarChar).Value = product.Description;
                    
                    cmd.ExecuteNonQuery();
                }
                using(SqlCommand cmd = new SqlCommand(command2,sqlconnection))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Product = new Product()
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetDecimal(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3)
                        };
                    }
                }
            }
            return Product;
            //_context.Products.Add(product);
            //await _context.SaveChangesAsync();
        }
        Product IAdminProductFunctions.UpdateProduct(Product request)
        {
            Product newProduct = request;
            string command = "UPDATE [Products] SET Value = @PValue,Name = @PName,Description = @PDescription WHERE Id = @PId";

            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                sqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    cmd.Parameters.Add("@PId", System.Data.SqlDbType.Int).Value = newProduct.Id;
                    cmd.Parameters.Add("@PValue", System.Data.SqlDbType.Decimal).Value = newProduct.Value;
                    cmd.Parameters.Add("@PName", System.Data.SqlDbType.NVarChar).Value = newProduct.Name;
                    cmd.Parameters.Add("@PDescription", System.Data.SqlDbType.NVarChar).Value = newProduct.Description;

                    cmd.ExecuteNonQuery();
                }
            }
                /*var product = _context.Products.FirstOrDefault(a => a.Id == request.Id);

                product.Name = request.Name;
                product.Description = request.Description;
                product.Value = request.Value;    

                await _context.SaveChangesAsync();*/
            return newProduct;
        }
        void IAdminProductFunctions.RemoveProduct(int id)
        {
            string command = "DELETE FROM [Products] WHERE Id = @Id";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                    cmd.ExecuteNonQuery();
                }
            }
                /*var product = _context.Products.FirstOrDefault(a => a.Id == id);
                _context.Products.Remove(product);

                await _context.SaveChangesAsync();*/
        }
        Product IAdminProductFunctions.GetProduct(int id)
        {
            Product Product = new Product();
            string command = "SELECT * FROM [Products] WHERE Id = @Id";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Product = new Product()
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetDecimal(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3)
                        };
                    }
                }
            }
            /*Product product = _context.Products.Where(a => a.Id == id).Select(a => new Product
            {
                Id = a.Id,
                Value = a.Value,
                Name = a.Name,
                Description = a.Description
            }).FirstOrDefault();*/
            return Product;
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
