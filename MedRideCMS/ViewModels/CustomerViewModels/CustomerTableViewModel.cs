using MedRideCMS.Models;
using MedRideCMS.ViewModels;
using MedRideCMS.ViewModels.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MedRideCMS.ViewModels.CustomerViewModels
{
    public class CustomerTableViewModel
    {
        public PagedResult<Customer> PagedResult { get; }

        public List<State> States { get; set; }
        public int SortByType { get; set; }
        public UrlHelper ReturnUrl { get; set; }
        public RouteValueDictionary ReturnRouteValues { get; set; }

        public bool HideFirstNameCol { get; set; }
        public bool HideLastNameCol { get; set; }
        public bool HideAddressCol { get; set; }
        public bool HideCityCol { get; set; }
        public bool HideStateCol { get; set; }
        public bool HideZipCol { get; set; }
        public bool HidePrimaryPhoneCol { get; set; }
        public bool HideSecondaryPhoneCol { get; set; }
        public bool HideEmailCol { get; set; }
        public bool HideUpdatedCol { get; set; }
        public bool HideCreatedCol { get; set; }

        public CustomerTableViewModel(PagedResult<Customer> pagedResult)
        {
            PagedResult = pagedResult;
        }
    }
}