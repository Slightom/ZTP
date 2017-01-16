using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Models.Classes;

namespace ZTP.Interfaces
{
    interface IReservation
    {
        bool IsTicketAvailable(int id);
        void ReserveTicket(int id, string userId, Enums.TicketType ticketType, double price);
    }
}
