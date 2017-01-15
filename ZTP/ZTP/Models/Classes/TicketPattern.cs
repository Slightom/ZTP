using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZTP.Models.Singleton;


namespace ZTP.Models.Classes
{
    public abstract class TicketPattern
    {
        private EmailService _emailService = new EmailService();
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

            var emailBody = GenerateMailBody(transportId, transport);
            var userEmail = _dbContext.Users.Where(x => x.Id.Equals(userId)).Select(x => x.Email).ToString();

            Task.Run(async () =>
            {
                await _emailService.SendEmailAsync(userEmail, emailBody, "Ticket Purchase");
            });

            return true;
        }

        private Ticket FlightTicket(int flightId, string userId)
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
            return ticket;
        }

        private Ticket TrainTicket(int trainId, string userId)
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

            return ticket;
        }

        private string GenerateMailBody(int transportId, Enums.TransportEnum transport)
        {
            var text = "Zakupiono bilet na trasę ";

            switch (transport)
            {
                case Enums.TransportEnum.Flight:
                {
                    var flight = _dbContext.Flights.First(x => x.FlightID == transportId);
                    text += flight.DepartureAirport.Name + " - " + flight.ArrivalAirport.Name + "\n";
                    text += "Dnia: " + flight.DepartureDate + "\n";
                    break;
                }
                case Enums.TransportEnum.Train:
                {
                    var train = _dbContext.Trains.First(x => x.TrainID == transportId);
                    text += train.DepartureStation.Name + " - " + train.ArrivalStation.Name + "\n";
                    text += "Dnia: " + train.DepartureDate + "\n";
                    break;
                }
            }

            return text;
        }

    }
}