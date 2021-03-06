﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Interface;

namespace Webshop.Data.Managers
{
    public class StockManager : IStockFunctions, IStock
    {
        private readonly string connectionstring;

        public StockManager(string conString)
        {
            connectionstring = conString;
        }
        StockDTO IStockFunctions.CreateStock(StockDTO stock)
        {
            StockDTO newStock = stock;
            string command = "INSERT INTO Stock (Quantity,Description,ProductId) OUTPUT INSERTED.Id VALUES(@SQuantity,@SDesc,@SProductId)";
            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                sqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    cmd.Parameters.Add("@SQuantity", System.Data.SqlDbType.Int).Value = newStock.Quantity;
                    cmd.Parameters.Add("@SDesc", System.Data.SqlDbType.NVarChar).Value = newStock.Description;
                    cmd.Parameters.Add("@SProductId", System.Data.SqlDbType.Int).Value = newStock.ProductId;

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        newStock = new StockDTO()
                        {
                            Id = reader.GetInt32(0),
                            Quantity = stock.Quantity,
                            Description = stock.Description,
                            ProductId = stock.ProductId
                        };
                    }
                }
            }
            return newStock;
        }
        IEnumerable<IStockFunctions.StockResponse> IStockFunctions.GetStock()
        {
            var stock = new List<IStockFunctions.StockResponse>();
            string command = "SELECT Id, Description, Name FROM [Products]";
            string command2 = "SELECT Quantity, Description, Id FROM [Stock] WHERE ProductId = @PId";

            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                sqlconnection.Open();
                using(SqlCommand cmd = new SqlCommand(command,sqlconnection))
                {
                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        stock.Add(new IStockFunctions.StockResponse()
                        {
                            Id = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            Name = reader.GetString(2)
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
                        List<StockDTO> stocks = new List<StockDTO>();
                        while (reader.Read())
                        {
                            stocks.Add(new StockDTO()
                            {
                                ProductId = s.Id,
                                Quantity = reader.GetInt32(0),
                                Description = reader.GetString(1),
                                Id = reader.GetInt32(2)
                            });
                        }
                        s.Stock = stocks;
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
                }
            }
            return stock;
        }
        void IStockFunctions.RemoveStock(int id)
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
        }

        void IStock.UpdateStock(StockDTO stock)
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
