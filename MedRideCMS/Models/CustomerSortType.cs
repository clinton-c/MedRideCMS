using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.Models
{
    public static class CustomerSortType
    {
        public const int Default = 0;
        public const int FirstName_Ascending = 1;
        public const int FirstName_Descending = 2;
        public const int LastName_Ascending = 3;
        public const int LastName_Descending = 4;
        public const int State_Ascending = 5;
        public const int State_Descending = 6;
    }
}