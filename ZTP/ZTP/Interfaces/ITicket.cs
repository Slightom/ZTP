using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Models.Classes;

namespace ZTP.Interfaces
{
    public interface ITicket
    {
        void SendEmail(int transportId, string userId, Enums.TransportEnum transport);
    }
}
