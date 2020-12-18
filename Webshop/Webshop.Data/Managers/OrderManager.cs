using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Interface;


namespace Webshop.Data.Managers
{
    public class OrderManager : IOrderFunctions
    {
        public OrderManager(string conString)
        {
            ConnectionString = conString;
        }

        private readonly string ConnectionString;

        OrderDTO IOrderFunctions.CreateOrder(OrderDTO order)
        {
            string command1 = "INSERT INTO Orders (OrderReference, Adress, Postcode, City) VALUES(@OrderReference,@Adress,@Postcode, @City)";
            string command2 = "SELECT * FROM [Orders] WHERE Id IN(SELECT Max(Id) FROM [Orders])";

            using (SqlConnection sqlconnection = new SqlConnection(ConnectionString))
            {
                sqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand(command1, sqlconnection))
                {
                    cmd.Parameters.Add("@OrderReference", System.Data.SqlDbType.NVarChar).Value = order.OrderReference;
                    cmd.Parameters.Add("@Adress", System.Data.SqlDbType.NVarChar).Value = order.Adress;
                    cmd.Parameters.Add("@Postcode", System.Data.SqlDbType.NVarChar).Value = order.Postcode;
                    cmd.Parameters.Add("@City", System.Data.SqlDbType.NVarChar).Value = order.City;

                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(command2, sqlconnection))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        order = new OrderDTO()
                        {
                            Id = reader.GetInt32(0),
                            OrderReference = reader.GetString(1),
                            Adress = reader.GetString(2),
                            Postcode = reader.GetString(3),
                            City = reader.GetString(4)
                        };
                    }
                }
            }
            return order;
        }

        IEnumerable<OrderDTO> IOrderFunctions.GetOrders()
        {
            List<OrderDTO> orders = new List<OrderDTO>();
            string command = "SELECT * FROM [Orders]";
            using (SqlConnection sqlconnection = new SqlConnection(ConnectionString))
            {
                sqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        orders.Add(new OrderDTO()
                        {
                            Id = reader.GetInt32(0),
                            OrderReference = reader.GetString(1),
                            Adress = reader.GetString(2),
                            Postcode = reader.GetString(3),
                            City = reader.GetString(4)
                        });
                    }
                }
            }
            return orders;
        }
        void IOrderFunctions.RemoveOrder(int id)
        {
            string command = "DELETE FROM [Orders] WHERE Id = @Id";
            using (SqlConnection sqlconnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlconnection))
                {
                    sqlconnection.Open();
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
