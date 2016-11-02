using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pizza_Application.Models
{
    /// <summary>
    /// Model to take in the users input
    /// </summary>
    public class HomeIndexViewModel
    {
        [Required(ErrorMessage = "The user name is required.")]
        public string UserName { set; get; }

        public string PhoneNumber { set; get; }
        
        public string Date { set; get; }

        public string Time { set; get; }

        [Range(1, 99)]
        public int NumOfPizzas { set; get; }
    }
}