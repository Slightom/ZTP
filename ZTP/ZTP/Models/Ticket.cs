using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Web;
using ZTP.Models.Classes;

namespace ZTP.Models
{
    public class Ticket
    {
        
        public int TicketID { get; set; }

        public int? FlightID { get; set; }
        public int? TrainID { get; set; }
        public string UserID { get; set; }

        public double Price { get; set; }
        public int SeatNumber { get; set; }
        public string Description { get; set; }


        public virtual Flight Flight { get; set; }
        public virtual Train Train { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}