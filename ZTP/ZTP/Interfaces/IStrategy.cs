using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZTP.Interfaces
{
    public interface IStrategy
    {
        ActionResult getView(string controller);
    }
}
