using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTP.Models.Classes;

namespace ZTP.Models.Facade
{
    public class TicketReservationFacade
    {
        private FlightReservationSystem _flightReservationSystem;
        private TrainReservationSystem _trainReservationSystem;

        public TicketReservationFacade()
        {
            _flightReservationSystem = new FlightReservationSystem();
            _trainReservationSystem = new TrainReservationSystem();
        }

        public bool ReserveTicket(int transportId, string userId, Enums.TransportEnum transport, Enums.TicketType ticketType, double price)
        {
            switch (transport)
            {
                case Enums.TransportEnum.Flight:
                    {
                        if (_flightReservationSystem.IsTicketAvailable(transportId))
                        {
                            _flightReservationSystem.ReserveTicket(transportId, userId, ticketType, price);
                            return true;
                        }
                        break;
                    }
                case Enums.TransportEnum.Train:
                    {

                        if (_trainReservationSystem.IsTicketAvailable(transportId))
                        {
                            _trainReservationSystem.ReserveTicket(transportId, userId, ticketType, price);
                            return true;
                        }
                        break;
                    }
            }

            return false;
        }
    }
}