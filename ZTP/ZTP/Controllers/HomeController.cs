using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTP.Interfaces;
using ZTP.Models.Classes;
using ZTP.Models.Facade;
using ZTP.Models.Strategy;

namespace ZTP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return GetView();
            //return View();
        }

        public ActionResult IndexList()
        {
            return View();
        }

        public ActionResult IndexTiles()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private ActionResult GetView()
        {
            StrategyContext sc = new StrategyContext(new StrategyList());
            return sc.ExecuteStrategy("Home");
        }


    }
}