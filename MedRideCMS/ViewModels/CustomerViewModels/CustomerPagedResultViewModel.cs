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
    public class CustomerPagedResultViewModel : PagedResultViewModel<Customer>
    {
        public List<State> States { get; set; }
        public int SortByType { get; set; }

        public UrlHelper ReturnUrl { get; set; }
        public RouteValueDictionary ReturnRouteValues { get; set; }

        public CustomerPagedResultViewModel(int currentPage, int pageSize, IEnumerable<Customer> dataSource) : base(currentPage, pageSize, dataSource)
        {
        }
    }
}