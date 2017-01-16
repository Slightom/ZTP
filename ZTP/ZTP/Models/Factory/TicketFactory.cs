using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Interfaces;
using ZTP.Models.Classes;

namespace ZTP.Models.Factory
{
    public static class TicketFactory
    {
        public static ITicket Create(Enums.TicketType ticketType, int transportId, string userId, Enums.TransportEnum transportType, double price)
        {
            ITicket result = null;
            switch (ticketType)
            {
                case Enums.TicketType.Concessionary:
                    {
                        var ticket = new ConcessionaryTicket(price);
                        ticket.GenerateTicket(transportId, userId, transportType);
                        result = ticket;
                        break;
                    }
                case Enums.TicketType.Normal:
                    {
                        var ticket = new NormalTicket(price);
                        ticket.GenerateTicket(transportId, userId, transportType);
                        result = ticket;
                        break;
                    }
            }
            return result;
        }
    }
}