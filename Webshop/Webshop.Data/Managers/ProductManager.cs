using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Interface;

namespace Webshop.Data
{
    public class ProductManager : IAdminProductFunctions, IUserProductFunctions, IProduct
    {
        
        private string connectionstring;

        public ProductManager(string conString)
        {
            connectionstring = conString;
        }
        ProductDTO IAdminProductFunctions.CreateProduct(ProductDTO product)
        {
            ProductDTO Product = new ProductDTO();
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
                        Product = new ProductDTO()
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
        }
        ProductDTO IProduct.UpdateProduct(ProductDTO request)
        {
            ProductDTO newProduct = request;
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
        }
        ProductDTO IUserProductFunctions.GetProduct(string name)
        {
            ProductDTO product = GetProductWithName(name);
            if(product == null)
            {
                return null;
            }
            string command = "SELECT Id, Description, Quantity FROM [Stock] WHERE ProductId = @Id";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = product.Id;

                    var reader = cmd.ExecuteReader();
                    product.Stock = new List<StockDTO>();
                    while (reader.Read())
                    {
                        product.Stock.Add(new StockDTO()
                        {
                            Id = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            Quantity = reader.GetInt32(2),
                        });
                        if(product.Stock[product.Stock.Count - 1].Quantity > 0)
                        {
                            product.Stock[product.Stock.Count - 1].InStock = true;
                        }
                    }
                }
            }
            return product;
        }
        ProductDTO GetProductWithName(string name)
        {
            ProductDTO Product = new ProductDTO();
            string command = "SELECT * FROM [Products] WHERE Name = @Name";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();
                    cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = name;

                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Product = new ProductDTO()
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetDecimal(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3)
                        };
                    }
                }
            }
            if(Product.Name == null)
            {
                return null;
            }
            return Product;
        }
        ProductDTO IAdminProductFunctions.GetProduct(int id)
        {
            ProductDTO Product = new ProductDTO();
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
                        Product = new ProductDTO()
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
        }

        IEnumerable<ProductDTO> IAdminProductFunctions.GetProducts()
        {
            return GetProducts();
        }
        IEnumerable<ProductDTO> IUserProductFunctions.GetProducts()
        {
            return GetProducts();
        }

        IEnumerable<ProductDTO> GetProducts()
        {
            string command = "SELECT * FROM [Products]";
            List<ProductDTO> products = new List<ProductDTO>();

            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                using(SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();

                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        products.Add(new ProductDTO()
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetDecimal(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3)
                        });
                    }
                }
            }
            return products;
        }
    }
}
