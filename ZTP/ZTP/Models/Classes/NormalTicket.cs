using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZTP.Models.Classes
{
    public class NormalTicket : TicketPattern
    {
        protected override double GetTicketPrice()
        {
            throw new NotImplementedException();
        }
    }
}