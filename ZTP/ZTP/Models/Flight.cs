using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZTP.Models
{
    public class Flight
    {
        public int FlightID { get; set; }
        public int DepartureAirportID { get; set; }
        public int ArrivalAirportID { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int NumberOfSeats { get; set; }
        public double Price { get; set; }

        public virtual Airport DepartureAirport { get; set; }
        public virtual Airport ArrivalAirport { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}