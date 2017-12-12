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

    }
}