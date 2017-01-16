using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTP.Models.Classes;

namespace ZTP.Models
{
    public class TrainViewModel
    {
        public Train Train { get; set; }
        public int TrainId { get; set; }
        public double Price { get; set; }
        public Enums.TicketType SelectedType { get; set; }

        public List<SelectListItem> TicketType { get; set; }

        public TrainViewModel() { }

        public TrainViewModel(Train train)
        {
            this.Train = train;
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