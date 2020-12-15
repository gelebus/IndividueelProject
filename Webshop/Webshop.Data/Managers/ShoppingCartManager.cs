using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Interface;

namespace Webshop.Data.Managers
{
    public class ShoppingCartManager : IShoppingCart
    {
        private string connectionstring;

        public ShoppingCartManager(string conString)
        {
            connectionstring = conString;
        }

        CartProductDTO IShoppingCart.GetCart(int stockId)
        {
            CartProductDTO cartProduct = new CartProductDTO();
            cartProduct.StockId = stockId;
            string command1 = "SELECT Quantity, ProductId FROM [Stock] Where Id = @SId";
            string command2 = "SELECT Name, Value FROM [Products] Where Id = @PId";
            int productId = 0;

            using (SqlConnection sqlconnection = new SqlConnection(connectionstring))
            {
                sqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand(command1, sqlconnection))
                {
                    cmd.Parameters.Add("@SId", System.Data.SqlDbType.Int).Value = cartProduct.StockId;

                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        cartProduct.Quantity = reader.GetInt32(0);
                        productId = reader.GetInt32(1);
                    }
                    reader.Close();
                }
                using (SqlCommand cmd = new SqlCommand(command2, sqlconnection))
                {
                    cmd.Parameters.Add("@PId", System.Data.SqlDbType.Int).Value = productId;
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cartProduct.Name = reader.GetString(0);
                        cartProduct.Value = $"€{reader.GetDecimal(1).ToString("N2")}";
                    }
                }
            }
            return cartProduct;
        }
    }
}
