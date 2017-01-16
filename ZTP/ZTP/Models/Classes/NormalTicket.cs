using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Interfaces;

namespace ZTP.Models.Classes
{
    public class NormalTicket : TicketPattern, ITicket
    {
        private double _price;
        protected override double GetTicketPrice()
        {
            return _price;
        }

        public NormalTicket() { }

        public NormalTicket(double price)
        {
            _price = price;
        }
    }
}