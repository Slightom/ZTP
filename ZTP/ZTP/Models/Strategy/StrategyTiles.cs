using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTP.Interfaces;

namespace ZTP.Models.Strategy
{
    public class StrategyTiles : Controller, IStrategy
    {
        public ActionResult getView(string controller)
        {
            return RedirectToAction("IndexTiles", controller);
        }
    }
}