using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels.CustomerViewModels
{
    public class CustomerDeletedViewModel
    {
        public Customer Customer { get; set; }

        public State State { get; set; }

        public string ReturnUrl { get; set; }
    }
}