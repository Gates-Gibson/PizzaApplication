using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pizza_Application.DataLayer;
using Pizza_Application.DataLayer.Entities;
using System.Collections.Generic;
using Pizza_Application.DataLayer.ADO;

namespace Pizza_Application.Tests
{
    [TestClass]
    public class ADOTests
    {
        private string _connectionString = null;
        private readonly IOrderRepository _orderRepository;

        public ADOTests()
        {
            string databaseFile = FileUtility.GetDatabaseFilePath();
            _connectionString = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
                          AttachDbFilename={0};
                          Integrated Security=True;
                          Connect Timeout=30", databaseFile);

            _orderRepository = new OrderRepository(_connectionString);
        }

        [TestMethod]
        public void GetAllOrders()
        {
            List<Order> orders = _orderRepository.GetAll();

            Assert.IsNotNull(orders);
            Assert.IsTrue(orders.Count > 0);

            foreach (var order in orders)
            {
                Console.WriteLine(order.Id + " - " + order.Name);
            }
        }

        [TestMethod]
        public void CreateNewOrder()
        {
            var order = new Order();
            order.Date = DateTime.UtcNow.ToString();
            order.Name = "Stuff";
            order.NumOfPizzas = "12";
            order.PhoneNumber = "123-123-1235";

            Assert.IsNull(order.Id);
            order = _orderRepository.Create(order);

            Assert.IsNotNull(order);
            Assert.IsNotNull(order.Id);
            Assert.IsNotNull(order.Date);
            Assert.IsNotNull(order.Name);
            Assert.IsNotNull(order.NumOfPizzas);
            Assert.IsNotNull(order.PhoneNumber);

            Console.WriteLine("Id = " + order.Id);
        }
    }
}
