using Pizza_Application.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Application.DataLayer.ADO
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            this._connectionString = connectionString;
        }

        public List<Order> GetAll()
        {
            string sql = "select * FROM [Order]";
            List<Order> orders = new List<Order>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string id = Convert.ToString(rdr["orderid"]);
                    string name = Convert.ToString(rdr["name"]);
                    string dateTime = Convert.ToString(rdr["datetime"]);
                    string phoneNumber = Convert.ToString(rdr["phoneNumber"]);
                    string numOfPizzas = Convert.ToString(rdr["numOfPizzas"]);

                    var order = new Order();
                    order.Id = id;
                    order.Name = name;
                    order.Date = dateTime;
                    order.PhoneNumber = phoneNumber;
                    order.NumOfPizzas = numOfPizzas;

                    orders.Add(order);
                }
            }

            return orders;
        }

        public Order Create(Order order)
        {
            string sql = @"INSERT INTO [Order](Name, PhoneNumber, DateTime, NumOfPizzas)
VALUES(@Name, @PhoneNumber, @DateTime, @NumOfPizzas);
select @@IDENTITY;";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@Name", order.Name));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", order.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@DateTime", order.Date));
                cmd.Parameters.Add(new SqlParameter("@NumOfPizzas", order.NumOfPizzas));

                var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    order.Id = rdr[0].ToString();
                }
            }

            return order;
        }
    }
}
