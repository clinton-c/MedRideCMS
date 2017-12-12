using MedRideCMS.DTOs;
using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels.CustomerViewModels
{
    public class CustomerLookupResultViewModel
    {
        public CustomerSearchParamsDto SearchParams { get; set; }
        public CustomerTableViewModel TableViewModel { get; set; }

    }
}