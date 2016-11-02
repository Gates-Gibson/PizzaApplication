using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Pizza_Application.Models;
using Pizza_Application.DataLayer;
using Pizza_Application.DataLayer.EntityFramwork;
using System.IO;
using System.Web.Hosting;
using System.Text.RegularExpressions;

namespace Pizza_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private static readonly Regex _phoneNumberFormat = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
        /// <summary>
        /// pass in the repository from Ninject
        /// </summary>
        /// <param name="orderRepository"></param>
        public HomeController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// pass the empty model to the view so that it can be used in the post
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string UserName)
        {
            var model = new HomeIndexViewModel()
            {
                //Date = "Today",
                //NumOfPizzas = 12,
                //PhoneNumber = "123-123-1234",
                //UserName = "Super Secret Username"
            };

            return View(model);
        }

        /// <summary>
        /// validate form information and if it's correct move to redirect to About.cshtml to view all orders
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(HomeIndexViewModel model)
        {
            bool validationFailed = false;
            if (ModelState.IsValid)
            {
                DateTime date = new DateTime();
                string dateString = "";
                dateString = model.Date + " " + model.Time;
                if (!DateTime.TryParse(dateString, out date))
                {
                    // we have a problem....
                    validationFailed = true;
                    ViewData.Add("ValidationError", "*The date was in the incorrect format");
                }
                else
                {
                    if (DateTime.Today.CompareTo(date) > 0)
                    {
                        validationFailed = true;
                        ViewData.Add("ValidationError", "*That Date is in the past");
                    }
                }
                if (!_phoneNumberFormat.IsMatch(model.PhoneNumber))
                {
                    validationFailed = true;
                    ViewData.Add("PhoneValidationError", "*Phone Number is incorrect");
                }
                if (model.UserName == "" || model.UserName == null)
                {
                    validationFailed = true;
                    ViewData.Add("UserNameValidationError", "*Name is empty");
                }

                if (model.NumOfPizzas < 1 || model.NumOfPizzas > 99)
                {
                    validationFailed = true;
                    ViewData.Add("NumOfPizzaValidationError", "*You can order from 1-99 pizzas.");
                }

                if (validationFailed == false)
                {
                    var order = new DataLayer.Entities.Order();
                    order.Name = model.UserName;
                    order.Date = dateString;
                    order.NumOfPizzas = model.NumOfPizzas.ToString();
                    order.PhoneNumber = model.PhoneNumber;

                    _orderRepository.Create(order);

                    /*
                     * Comment out redirect and remove the comment from the line below to be able to add multiple orders in a row
                     * return View(model);
                     */
                    return RedirectToAction("About", "Home");
                }
            }
            else
            {
                ViewData.Add("EmptyFieldValidationError", "*Not all fields have been filled");
            }
            return View(model);
        }
        /// <summary>
        /// Converts db info into the proper format to be displayed to the user
        /// </summary>
        /// <returns>the list orders</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Orders";
            List<Models.OrderViewModel> orders = new List<Models.OrderViewModel>();
            foreach(var item in _orderRepository.GetAll())
            {
                Models.OrderViewModel x = new Models.OrderViewModel();
                x.Name = item.Name;
                x.PhoneNumber = item.PhoneNumber;
                x.DateTime = item.Date;
                x.NumOfPizzas = Convert.ToInt32(item.NumOfPizzas);
                orders.Add(x); 
            }
            return View(orders);
        }

        /// <summary>
        /// unused controller generated by initial project set up.
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
 