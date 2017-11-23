using MedRideCMS.DTOs;
using MedRideCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels
{
    public class CustomerLookupViewModel
    {
        public CustomerSearchParamsDto SearchParams { get; set; }

        public List<State> States { get; set; }
    }
}