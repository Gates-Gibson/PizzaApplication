using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_Application.DataLayer.Entities;

namespace Pizza_Application.DataLayer.EntityFramwork
{
    public class EfOrderRepository : IOrderRepository
    {
        public Entities.Order Create(Entities.Order order)
        {
            PizzaOrderDatabaseEntities x = new PizzaOrderDatabaseEntities();
            Order efOrder = new Order();
            efOrder.name = order.Name;
            efOrder.numOfPizzas = Convert.ToInt32(order.NumOfPizzas);
            efOrder.dateTime = order.Date;
            efOrder.phoneNumber = order.PhoneNumber;
            x.Orders.Add(efOrder);
            x.SaveChanges();
            order.Id = efOrder.orderId.ToString();
            return order;
            }

        public List<Entities.Order> GetAll()
        {
            PizzaOrderDatabaseEntities x = new PizzaOrderDatabaseEntities();
            List<Entities.Order> items = new List<Entities.Order>();
            foreach(var item in x.Orders)
            {
                Entities.Order order = new Entities.Order();
                order.Name = item.name;
                order.PhoneNumber = item.phoneNumber;
                order.NumOfPizzas = item.numOfPizzas.ToString();
                order.Date = item.dateTime;
                items.Add(order);
            }
            return items;
        }

        public void test()
        {
            //PizzaOrderDatabaseEntities x = new PizzaOrderDatabaseEntities();
            //try
            //{


            //    var items = x.Orders.Where(d => d.name == "Gates Gibson");
            //    var items2 = items.ToList();
            //    foreach (var item in items)
            //    {

            //    }
            //}catch(Exception ex)
            //{
                
            //}
        }
    }
}
