using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string SecondaryPhone { get; set; }

        public string Email { get; set; }


        public override string ToString()
        {
            var str = FirstName
                + " " + LastName
                + " " + Address
                + " " + City
                + " ";

            str += State != null ? State.AbbreviatedName + " " : string.Empty;

            str +=  Zip
                + " " + Phone
                + " " + SecondaryPhone
                + " " + Email;

            return str;
        }
    }
}