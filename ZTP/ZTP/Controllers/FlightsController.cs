using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ZTP.Models;
using ZTP.Models.Strategy;
using PagedList;
using ZTP.Models.Classes;
using ZTP.Models.Factory;

namespace ZTP.Controllers
{
    public class FlightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            //var flights = db.Flights.Include(f => f.ArrivalAirport).Include(f => f.DepartureAirport);
            //return View(flights.ToList());
            return GetView();
        }


        public ActionResult IndexList(string searchString, int? page, SearchModel sm)


        {
            var flights = db.Flights.Include(p=>p.ArrivalAirport).Include(p=>p.DepartureAirport);

            if (searchString != null) // if standard search
            {
                flights = flights.Where(p => p.DepartureAirport.Location.Contains(searchString) || p.ArrivalAirport.Location.Contains(searchString));
            }

            if(sm != null && sm.From!=sm.To) // if extentend search
            {
                flights = flights.Where(p => p.DepartureAirport.Location == sm.From && p.ArrivalAirport.Location == sm.To);
                ViewBag.SM = sm;
                ViewBag.Filters = "unset;";
            }
            else { ViewBag.Filters = "none;"; }


            flights = flights.OrderByDescending(p => p.DepartureDate);
            return View(flights.ToList().ToPagedList(page ?? 1, 4));

        }

        public ActionResult IndexTiles(string searchString, int? page, SearchModel sm)
        {
            var flights = db.Flights.Include(p => p.ArrivalAirport).Include(p => p.DepartureAirport);

            if (searchString != null) // if standard search
            {
                flights = flights.Where(p => p.DepartureAirport.Location.Contains(searchString) || p.ArrivalAirport.Location.Contains(searchString));
            }

            if (sm != null && sm.From != sm.To) // if extentend search
            {
                flights = flights.Where(p => p.DepartureAirport.Location == sm.From && p.ArrivalAirport.Location == sm.To);
                ViewBag.SM = sm;
                ViewBag.Filters = "unset;";
            }
            else { ViewBag.Filters = "none;"; }


            flights = flights.OrderByDescending(p => p.DepartureDate);

            return View(flights.ToList().ToPagedList(page ?? 1, 4));
        }



        #region details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            var vm = new FlightViewModel(flight);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(FlightViewModel flightViewModel)
        {
            if (ModelState.IsValid)
            {
                var flightId = flightViewModel.Flight.FlightID;
                var userId = this.HttpContext.User.Identity.GetUserId();
                var price = flightViewModel.Flight.Price;
                var ticketType = (Enums.TicketType)flightViewModel.SelectedType;

                var ticket = TicketFactory.Create(ticketType, flightId, userId, Enums.TransportEnum.Flight, price);
                ticket.SendEmail(flightId, userId, Enums.TransportEnum.Flight);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index"); // zmienić na error page
        }
        #endregion

        #region create
        public ActionResult Create()
        {
            ViewBag.ArrivalAirportID = new SelectList(db.Airports, "AirportID", "Name");
            ViewBag.DepartureAirportID = new SelectList(db.Airports, "AirportID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlightID,DepartureAirportID,ArrivalAirportID,DepartureDate,ArrivalDate,NumberOfSeats,Price")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArrivalAirportID = new SelectList(db.Airports, "AirportID", "Name", flight.ArrivalAirportID);
            ViewBag.DepartureAirportID = new SelectList(db.Airports, "AirportID", "Name", flight.DepartureAirportID);
            return View(flight);
        }
        #endregion

        #region edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArrivalAirportID = new SelectList(db.Airports, "AirportID", "Name", flight.ArrivalAirportID);
            ViewBag.DepartureAirportID = new SelectList(db.Airports, "AirportID", "Name", flight.DepartureAirportID);
            return View(flight);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightID,DepartureAirportID,ArrivalAirportID,DepartureDate,ArrivalDate,NumberOfSeats,Price")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArrivalAirportID = new SelectList(db.Airports, "AirportID", "Name", flight.ArrivalAirportID);
            ViewBag.DepartureAirportID = new SelectList(db.Airports, "AirportID", "Name", flight.DepartureAirportID);
            return View(flight);
        }
        #endregion

        #region delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private ActionResult GetView()
        {
            if (Session["Strategy"] == null)
            {
                StrategyContext sc = new StrategyContext(new StrategyList());
                Session["Strategy"] = sc;
            }


            return ((StrategyContext)Session["Strategy"]).ExecuteStrategy("Flights");//sc.ExecuteStrategy("Home");
        }

        public ActionResult TilesDisplay()
        {

            StrategyContext sc = new StrategyContext(new StrategyTiles());
            Session["Strategy"] = sc;
            
            return ((StrategyContext)Session["Strategy"]).ExecuteStrategy("Flights");//sc.ExecuteStrategy("Home");
        }

        public ActionResult ListDisplay()
        {

            StrategyContext sc = new StrategyContext(new StrategyList());
            Session["Strategy"] = sc;

            return ((StrategyContext)Session["Strategy"]).ExecuteStrategy("Flights");//sc.ExecuteStrategy("Home");
        }


        public ActionResult GenerateSearchModel(SearchModel model)
        {
            // generate list of locations without duplicates
            List<string> ls = db.Airports.Select(p => p.Location).Distinct().ToList();
            IEnumerable<SelectListItem> lss2;
            List<SelectListItem> ls2 = new List<SelectListItem>();
            foreach (var l in ls)
            {
                ls2.Add(new SelectListItem() { Text = l, Value = l });
            }
            lss2 = ls2;
            ViewBag.From = new SelectList(lss2, "Value", "Value");
            ViewBag.To = new SelectList(lss2, "Value", "Value");
            ////////////////////////////////////////////////////////////////////////////////////////

            SearchModel sm = new SearchModel();
            if (model != null)
            {
                if (model.From != null)
                {
                    ViewBag.From = new SelectList(lss2, "Value", "Value", model.From);
                }

                if (model.To != null)
                {
                    ViewBag.To = new SelectList(lss2, "Value", "Value", model.To);
                }
            }


            return View(sm);
        }

      
    }
}
