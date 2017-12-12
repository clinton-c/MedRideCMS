using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedRideCMS.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customers first name is required!")]
        [Display(Name = "First Name")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Customers last name is required!")]
        [Display(Name = "Last Name")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Customers address is required!")]
        [Display(Name = "Address")]
        [StringLength(255)]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Customers state is required!")]
        public int StateId { get; set; }

        [Display(Name = "State")]
        public State State { get; set; }

        [Required(ErrorMessage = "Customers city is required!")]
        [Display(Name = "City")]
        [StringLength(255)]
        public string City { get; set; }

        [Required(ErrorMessage = "Customers zipcode is required!")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid zipcode")]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please Provide Customers primary phone number!")]
        [Display(Name = "Primary Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid phone number")]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Secondary Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid phone number")]
        [Phone]
        public string SecondaryPhone { get; set; }

        [Display(Name = "Email")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime Updated { get; set; }

        [Required]
        public DateTime Created { get; private set; }


        public Customer()
        {
            Created = DateTime.Now;
            Updated = Created;
        }


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