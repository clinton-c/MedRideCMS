using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.ViewModels.CustomerViewModels
{
    public class IndexViewModel
    {
        public CustomerTableViewModel TableViewModel { get; set; }

        // These properties are only used when submitting form data
        public int SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}