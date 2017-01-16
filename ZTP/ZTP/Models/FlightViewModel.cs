using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTP.Models.Classes;

namespace ZTP.Models
{
    public class FlightViewModel
    {
        public Flight Flight { get; set; }
        public int FlightId { get; set; }
        public double Price { get; set; }
        public Enums.TicketType SelectedType { get; set; }

        public List<SelectListItem> TicketType { get; set; }

        public FlightViewModel() { }

        public FlightViewModel(Flight flight)
        {
            this.Flight = flight;
            TicketType = new List<SelectListItem>();
            TicketType.Add(new SelectListItem()
            {
                Text = "Concessionary",
                Value = Enums.TicketType.Concessionary.ToString()
            });
            TicketType.Add(new SelectListItem() { Text = "Normal", Value = Enums.TicketType.Normal.ToString() });
        }
    }
}