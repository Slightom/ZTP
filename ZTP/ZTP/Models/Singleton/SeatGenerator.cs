using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZTP.Models.Singleton
{
    public class SeatGenerator
    {
        private static SeatGenerator _seatGenerator = new SeatGenerator();

        private SeatGenerator() { }

        public static SeatGenerator GetInstance()
        {
            return _seatGenerator;
        }

        public int GetSeatNumber(ApplicationDbContext db, int id)
        {
            var flight = db.Flights.First(x => x.FlightID == id);
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
            return 0;
        }
    }
}