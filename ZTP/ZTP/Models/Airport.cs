using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZTP.Models
{
    public class Airport
    {
        public int AirportID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhotoPath { get; set; }


        public virtual ICollection<Flight> FlightsFrom { get; set; }
        public virtual ICollection<Flight> FlightsTo { get; set; }
    }
}