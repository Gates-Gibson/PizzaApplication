using Pizza_Application.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Application.DataLayer
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order Create(Order order);
    }
    
}
