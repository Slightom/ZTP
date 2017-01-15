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

        public ActionResult Test()
        {
            TicketReservationFacade fasada = new TicketReservationFacade();
            fasada.ReserveTicket(1, "8887934e-c0a3-4eab-bca5-5556dfee1700", Enums.TransportEnum.Flight, Enums.TicketType.Normal);

            return View();
        }


    }
}