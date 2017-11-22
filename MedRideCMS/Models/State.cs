using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedRideCMS.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AbbreviatedName { get; set; }
        public bool HasCoverage { get; set; }

        public static readonly int Florida = 1;
        public static readonly int Texas = 2;
        public static readonly int Michigan = 3;
        public static readonly int California = 4;
        public static readonly int NewYork = 5;
        public static readonly int Colorado = 6;
        public static readonly int Pennsylvania = 7;
       
    }
}