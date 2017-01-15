using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Interfaces;
using ZTP.Models.Singleton;

namespace ZTP.Models.Facade
{
    public class TrainReservationSystem : IReservation
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public bool IsTicketAvailable(int id)
        {
            var train = _dbContext.Trains.First(x => x.TrainID == id);
            var seatsTaken = train.Tickets.Count;

            return seatsTaken < train.NumberOfSeats;
        }

        public void ReserveTicket(int trainId, string userId)
        {
            var seat = SeatGenerator.GetInstance().GetSeatNumber(trainId, "train");
            var train = _dbContext.Trains.First(x => x.TrainID == trainId);
            var ticket = new Ticket()
            {
                TrainID = trainId,
                SeatNumber = seat,
                Price = train.Price,
                UserID = userId
            };

            _dbContext.Tickets.Add(ticket);
        }
    }
}