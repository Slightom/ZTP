using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Interfaces;
using ZTP.Models.Singleton;

namespace ZTP.Models.Facade
{
    public class FlightReservationSystem : IReservation
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public bool IsTicketAvailable(int id)
        {
            var flight = _dbContext.Flights.First(x => x.FlightID == id);
            var seatsTaken = flight.Tickets.Count;

            return seatsTaken < flight.NumberOfSeats;
        }

        public void ReserveTicket(int flightId, string userId)
        {
            var seat = SeatGenerator.GetInstance().GetSeatNumber(flightId, "flight");
            var flight = _dbContext.Flights.First(x => x.FlightID == flightId);
            var ticket = new Ticket()
            {
                FlightID = flightId,
                SeatNumber = seat,
                Price = flight.Price,
                UserID = userId
            };

            _dbContext.Tickets.Add(ticket);
        }
    }
}