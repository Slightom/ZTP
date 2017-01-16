using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZTP.Interfaces;

namespace ZTP.Models.Classes
{
    public class ConcessionaryTicket : TicketPattern, ITicket
    {
        private double _price;
        private EmailService _emailService = new EmailService();
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        protected override double GetTicketPrice()
        {
            return Math.Round(_price/2, 2, MidpointRounding.AwayFromZero);
        }

        public ConcessionaryTicket() { }

        public ConcessionaryTicket(double price)
        {
            _price = price;
        }

        public void SendEmail(int transportId, string userId, Enums.TransportEnum transport)
        {
            var emailBody = GenerateMailBody(transportId, transport);
            var user = _dbContext.Users.Single(x => x.Id.Equals(userId));
            var userEmail = user.Email;

            Task.Run(async () =>
            {
                await _emailService.SendEmailAsync(userEmail, emailBody, "Ticket Purchase");
            });
        }

        private string GenerateMailBody(int transportId, Enums.TransportEnum transport)
        {
            var text = "Zakupiono bilet ulgowy na trasę ";

            switch (transport)
            {
                case Enums.TransportEnum.Flight:
                    {
                        var flight = _dbContext.Flights.Single(x => x.FlightID == transportId);
                        text += flight.DepartureAirport.Name + " - " + flight.ArrivalAirport.Name + "\n";
                        text += "Dnia: " + flight.DepartureDate + "\n";
                        break;
                    }
                case Enums.TransportEnum.Train:
                    {
                        var train = _dbContext.Trains.Single(x => x.TrainID == transportId);
                        text += train.DepartureStation.Name + " - " + train.ArrivalStation.Name + "\n";
                        text += "Dnia: " + train.DepartureDate + "\n";
                        break;
                    }
            }

            return text;
        }
    }
}