using MedRideCMS.DTOs;
using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels
{
    public class CustomerSearchResultViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<State> States { get; set; }
        public CustomerSearchParamsDto SearchParams { get; set; }
        public string SortByType { get; set; }
        public PageHandler<Customer> PageHandler { get; set; }
        public string ReturnUrl { get; set; }

        public const string FirstName_Ascending = "firstName_ascending";
        public const string FirstName_Descending = "firstName_descending";
        public const string LastName_Ascending = "lastName_ascending";
        public const string LastName_Descending = "lastName_descending";
        public const string State_Ascending = "state_ascending";
        public const string State_Descending = "state_descending";
    }
}