using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTP.Interfaces;

namespace ZTP.Models.Strategy
{
    public class StrategyContext
    {
        private IStrategy _strategy;

        public StrategyContext(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public ActionResult ExecuteStrategy(string controller)
        {
            return _strategy.getView(controller);
        }
    }
}