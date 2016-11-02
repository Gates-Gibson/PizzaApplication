using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Application.DataLayer.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }
        public string NumOfPizzas { get; set; }
    }
}
