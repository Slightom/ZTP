﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Models.Classes;

namespace ZTP.Models.Singleton
{
    public class SeatGenerator
    {
        private static SeatGenerator _seatGenerator = new SeatGenerator();
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        private SeatGenerator() { }

        public static SeatGenerator GetInstance()
        {
            return _seatGenerator;
        }

        public int GetSeatNumber(int id, Enums.TransportEnum transport)
        {
            switch (transport)
            {
                case Enums.TransportEnum.Train:
                    {
                        var flight = _dbContext.Flights.First(x => x.FlightID == id);
                        var seatsTaken = flight.Tickets.Count;

                        if (seatsTaken < flight.NumberOfSeats)
                        {
                            var seatsList = flight.Tickets.Select(ticket => ticket.SeatNumber).ToList();

                            for (int i = 1; i < seatsTaken; i++)
                            {
                                if (!seatsList.Contains(i)) return i;
                            }

                            seatsTaken++;
                            return seatsTaken;
                        }
                        break;
                }
                case Enums.TransportEnum.Flight:
                {
                        var train = _dbContext.Trains.First(x => x.TrainID == id);
                        var seatsTaken = train.Tickets.Count;

                        if (seatsTaken < train.NumberOfSeats)
                        {
                            var seatsList = train.Tickets.Select(ticket => ticket.SeatNumber).ToList();

                            for (int i = 1; i < seatsTaken; i++)
                            {
                                if (!seatsList.Contains(i)) return i;
                            }

                            seatsTaken++;
                            return seatsTaken;
                        }
                        break;
                }
            }
            
            return 0;
        }
    }
}