using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels.CustomerViewModels
{
    public class NewCustomerViewModel
    {
        public Customer Customer { get; set; }

        public List<State> States { get; set; }
    }
}