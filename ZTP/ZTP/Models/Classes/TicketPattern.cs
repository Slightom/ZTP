using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZTP.Interfaces;
using ZTP.Models.Singleton;


namespace ZTP.Models.Classes
{
    public abstract class TicketPattern
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        protected abstract double GetTicketPrice();

        public bool GenerateTicket(int transportId, string userId, Enums.TransportEnum transport)
        {
            Ticket ticket = null;
            var price = GetTicketPrice();

            switch (transport)
            {
                case Enums.TransportEnum.Flight:
                {
                    ticket = FlightTicket(transportId, userId);
                    break;
                }
                case Enums.TransportEnum.Train:
                {
                    ticket = TrainTicket(transportId, userId);
                    break;
                }
            }

            if (ticket == null) return false;

            ticket.Price = price;
            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();

            ApplicationUser u = _dbContext.Users.Find(ticket.UserID);
            u.AvailableFunds -= ticket.Price;
            _dbContext.SaveChanges();

            return true;
        }

        private Ticket FlightTicket(int flightId, string userId)
        {
            var seat = SeatGenerator.GetInstance().GetSeatNumber(flightId, Enums.TransportEnum.Flight);
            var flight = _dbContext.Flights.Single(x => x.FlightID == flightId);
            var ticket = new Ticket()
            {
                FlightID = flightId,
                SeatNumber = seat,
                Price = flight.Price,
                UserID = userId
            };
            return ticket;
        }

        private Ticket TrainTicket(int trainId, string userId)
        {
            var seat = SeatGenerator.GetInstance().GetSeatNumber(trainId, Enums.TransportEnum.Train);
            var train = _dbContext.Trains.Single(x => x.TrainID == trainId);
            var ticket = new Ticket()
            {
                TrainID = trainId,
                SeatNumber = seat,
                Price = train.Price,
                UserID = userId
            };

            return ticket;
        }

        

    }
}