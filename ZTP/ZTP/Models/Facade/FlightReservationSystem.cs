using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Interfaces;
using ZTP.Models.Classes;
using ZTP.Models.Singleton;

namespace ZTP.Models.Facade
{
    public class FlightReservationSystem : IReservation
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public bool IsTicketAvailable(int id)
        {
            var flight = _dbContext.Flights.Single(x => x.FlightID == id);
            var seatsTaken = flight.Tickets.Count;

            return seatsTaken < flight.NumberOfSeats;
        }

        public void ReserveTicket(int flightId, string userId, Enums.TicketType ticketType)
        {
            switch (ticketType)
            {
                case Enums.TicketType.Concessionary:
                    {
                        var ticket = new ConcessionaryTicket();
                        ticket.GenerateTicket(flightId, userId, Enums.TransportEnum.Flight);
                        break;
                    }
                case Enums.TicketType.Normal:
                    {
                        var ticket = new NormalTicket();
                        ticket.GenerateTicket(flightId, userId, Enums.TransportEnum.Flight);
                        break;
                    }
            }
        }
    }
}