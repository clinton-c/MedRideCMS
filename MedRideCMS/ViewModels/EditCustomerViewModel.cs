using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels
{
    public class EditCustomerViewModel
    {
        public Customer Customer { get; set; }

        public List<State> States { get; set; }

        public string ReturnUrl { get; set; }
    }
}