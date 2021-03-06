﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Interfaces;
using ZTP.Models.Classes;
using ZTP.Models.Singleton;

namespace ZTP.Models.Facade
{
    public class TrainReservationSystem : IReservation
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public bool IsTicketAvailable(int id)
        {
            var train = _dbContext.Trains.Single(x => x.TrainID == id);
            var seatsTaken = train.Tickets.Count;

            return seatsTaken < train.NumberOfSeats;
        }

        public void ReserveTicket(int trainId, string userId, Enums.TicketType ticketType, double price)
        {
            switch (ticketType)
            {
                case Enums.TicketType.Concessionary:
                    {
                        var ticket = new ConcessionaryTicket(price);
                        ticket.GenerateTicket(trainId, userId, Enums.TransportEnum.Train);
                        break;
                    }
                case Enums.TicketType.Normal:
                    {
                        var ticket = new NormalTicket(price);
                        ticket.GenerateTicket(trainId, userId, Enums.TransportEnum.Train);
                        break;
                    }
            }
        }
    }
}