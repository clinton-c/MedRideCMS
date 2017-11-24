using MedRideCMS.DTOs;
using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels
{
    public class CustomerLookupResultViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<State> States { get; set; }
        public CustomerSearchParamsDto SearchParams { get; set; }
        public int SortByType { get; set; }
        public PageHandler<Customer> PageHandler { get; set; }
        public string ReturnUrl { get; set; }

    }
}