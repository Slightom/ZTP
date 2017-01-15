using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZTP.Models
{
    public class Train
    {
        public int TrainID { get; set; }
        public int DepartureStationID { get; set; }
        public int ArrivalStationID { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int NumberOfSeats { get; set; }
        public double Price { get; set; }

        public virtual Station DepartureStation{ get; set; }
        public virtual Station ArrivalStation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}