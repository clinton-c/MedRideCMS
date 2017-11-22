using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.DTOs
{
    public class CustomerSearchParamsDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public int? StateId { get; set; }

        public string StateName { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public override string ToString()
        {
            var str = FirstName + " " + LastName + " ";

            if(!string.IsNullOrWhiteSpace(Address))
                str += Address + " ";

            if (!string.IsNullOrWhiteSpace(City))
                str += City + " ";

            if (!string.IsNullOrWhiteSpace(StateName))
                str += StateName + " ";

            if (!string.IsNullOrWhiteSpace(Zip))
                str += Zip;

                return str;
        }

    }
}