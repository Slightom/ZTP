using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Interfaces;

namespace ZTP.Models.Classes
{
    public class ConcessionaryTicket : TicketPattern, ITicket
    {
        private double _price;
        protected override double GetTicketPrice()
        {
            return Math.Round(_price/2, 2, MidpointRounding.AwayFromZero);
        }

        public ConcessionaryTicket() { }

        public ConcessionaryTicket(double price)
        {
            _price = price;
        }
    }
}