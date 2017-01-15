using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public bool ReserveTicket(int transportId, string userId, string transport)
        {
            switch (transport)
            {
                case "flight":
                    {
                        if (_flightReservationSystem.IsTicketAvailable(transportId))
                        {
                            _flightReservationSystem.ReserveTicket(transportId, userId);
                            return true;
                        }
                        break;
                    }
                case "train":
                    {

                        if (_trainReservationSystem.IsTicketAvailable(transportId))
                        {
                            _trainReservationSystem.ReserveTicket(transportId, userId);
                            return true;
                        }
                        break;
                    }
            }

            return false;
        } 
    }
}