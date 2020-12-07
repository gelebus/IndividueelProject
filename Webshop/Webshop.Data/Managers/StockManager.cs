using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Interface;

namespace Webshop.Data.Managers
{
    public class StockManager : IAdminStockFunctions
    {
        private AppDbContext _context;
        string connectionstring;

        public StockManager(AppDbContext context)
        {
            _context = context;
            connectionstring = context.Database.GetDbConnection().ConnectionString;
        }
        Stock IAdminStockFunctions.CreateStock(Stock stock)
        {
            Stock newStock = stock;
            string command1 = "INSERT INTO Stock (Quantity,Description,ProductId) VALUES(@SQuantity,@SDesc,@SProductId)";
            string command2 = "SELECT * FROM [Stock] WHERE Id IN(SELECT Max(Id) FROM [Stock])";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                sqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand(command1, sqlconnection))
                {
                    cmd.Parameters.Add("@SQuantity", System.Data.SqlDbType.Int).Value = newStock.Quantity;
                    cmd.Parameters.Add("@SDesc", System.Data.SqlDbType.NVarChar).Value = newStock.Description;
                    cmd.Parameters.Add("@SProductId", System.Data.SqlDbType.Int).Value = newStock.ProductId;

                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(command2, sqlconnection))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        newStock = new Stock()
                        {
                            Id = reader.GetInt32(0),
                            Quantity = reader.GetInt32(1),
                            Description = reader.GetString(2),
                            ProductId = reader.GetInt32(3)
                        };
                    }
                }
            }
            return newStock;
            //_context.Stock.Add(stock);
            //await _context.SaveChangesAsync();
        }
        IEnumerable<IAdminStockFunctions.StockResponse> IAdminStockFunctions.GetStock()
        {
            var stock = new List<IAdminStockFunctions.StockResponse>();
            string command = "SELECT Id, Description FROM [Products]";
            string command2 = "SELECT Quantity, Description FROM [Stock] WHERE ProductId = @PId";

            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                sqlconnection.Open();
                using(SqlCommand cmd = new SqlCommand(command,sqlconnection))
                {
                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        stock.Add(new IAdminStockFunctions.StockResponse()
                        {
                            Id = reader.GetInt32(0),
                            Description = reader.GetString(1)
                        });
                    }
                    reader.Close();
                }
                using (SqlCommand cmd = new SqlCommand(command2, sqlconnection))
                {
                    foreach (var s in stock)
                    {
                        cmd.Parameters.Add("@PId", System.Data.SqlDbType.Int).Value = s.Id;
                        var reader = cmd.ExecuteReader();
                        List<Stock> stocks = new List<Stock>();
                        while (reader.Read())
                        {
                            stocks.Add(new Stock()
                            {
                                Quantity = reader.GetInt32(0),
                                Description = reader.GetString(1)
                            });
                        }
                        s.Stock = stocks;
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
                }
            }
            /*var stock = _context.Products
                .Include(a => a.Stock)
                .Select(a => new IAdminStockFunctions.StockResponse
                {
                    Id = a.Id,
                    Description = a.Description,
                    Stock = a.Stock.Select(b => new Stock
                    {
                        Id = b.Id,
                        Description = b.Description,
                        Quantity = b.Quantity,
                    })
                })
                .ToList();

            return stock;*/
            return stock;
        }
        void IAdminStockFunctions.RemoveStock(int id)
        {
            string command = "DELETE FROM [Stock] WHERE Id = @SId";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();
                    cmd.Parameters.Add("@SId", System.Data.SqlDbType.Int).Value = id;

                    cmd.ExecuteNonQuery();
                }
            }

            /*var stock = _context.Stock.FirstOrDefault(a => a.Id == id);
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();*/
        }
        void IAdminStockFunctions.UpdateStock(IEnumerable<Stock> Stock)
        {
            foreach (var stock in Stock)
            {
                UpdateStock(stock);
            }
            /*_context.UpdateRange(stock);
            await _context.SaveChangesAsync();*/
        }

        void UpdateStock(Stock stock)
        {
            string command = "UPDATE Stock SET Quantity = @SQuantity, Description = @SDescription WHERE Id = @SId";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();
                    cmd.Parameters.Add("@SQuantity", System.Data.SqlDbType.Int).Value = stock.Quantity;
                    cmd.Parameters.Add("@SDescription", System.Data.SqlDbType.NVarChar).Value = stock.Description;
                    cmd.Parameters.Add("@SId", System.Data.SqlDbType.Int).Value = stock.Id;

                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
