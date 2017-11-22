using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels
{
    public class SaveCustomerFailedViewModel
    {
        public Customer Customer { get; set; }
        public State State { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }
}