using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Pizza_Application.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new IndexViewModel);
        }

        public ActionResult About()
        {
            PizzaOrderDatabaseEntities dbe = new PizzaOrderDatabaseEntities();
            ViewBag.Message = "Orders";

            return View(dbe.Orders.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public class IndexViewModel
        {
            public string Name { set; get; }
            public string phoneNumber { set; get;}
            public string dateTime { set; get; }
            public string numOfPizzas { set; get; }
        }
    }
}