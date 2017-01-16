using PagedList;
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
using ZTP.Models.Classes;
using ZTP.Models.Factory;
using ZTP.Models.Strategy;

namespace ZTP.Controllers
{
    public class TrainsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return GetView();
        }


        public ActionResult IndexList(string searchString, int? page, SearchModel sm)
        {
            var trains = db.Trains.Include(p => p.ArrivalStation).Include(p => p.DepartureStation);

            if (searchString != null) // if standard search
            {
                trains = trains.Where(p => p.DepartureStation.Location.Contains(searchString) || p.ArrivalStation.Location.Contains(searchString));
            }

            if (sm != null && sm.From != sm.To) // if extentend search
            {
                trains = trains.Where(p => p.DepartureStation.Location == sm.From && p.ArrivalStation.Location == sm.To);
                ViewBag.SM = sm;
                ViewBag.Filters = "unset;";
            }
            else { ViewBag.Filters = "none;"; }


            trains = trains.OrderByDescending(p => p.DepartureDate);
            return View(trains.ToList().ToPagedList(page ?? 1, 4));

        }

        public ActionResult IndexTiles(string searchString, int? page, SearchModel sm)
        {
            var trains = db.Trains.Include(p => p.ArrivalStation).Include(p => p.DepartureStation);

            if (searchString != null) // if standard search
            {
                trains = trains.Where(p => p.DepartureStation.Location.Contains(searchString) || p.ArrivalStation.Location.Contains(searchString));
            }

            if (sm != null && sm.From != sm.To) // if extentend search
            {
                trains = trains.Where(p => p.DepartureStation.Location == sm.From && p.ArrivalStation.Location == sm.To);
                ViewBag.SM = sm;
                ViewBag.Filters = "unset;";
            }
            else { ViewBag.Filters = "none;"; }


            trains = trains.OrderByDescending(p => p.DepartureDate);
            return View(trains.ToList().ToPagedList(page ?? 1, 4));
        }


        #region details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train train = db.Trains.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            var vm = new TrainViewModel(train);

            int takenTicket = db.Tickets.Where(p => p.TrainID == train.TrainID).Count();
            ViewBag.AvailableTickets = train.NumberOfSeats - takenTicket;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(TrainViewModel trainViewModel)
        {
            if (ModelState.IsValid)
            {
                var trainId = trainViewModel.Train.TrainID;
                var userId = this.HttpContext.User.Identity.GetUserId();
                var price = trainViewModel.Train.Price;
                var ticketType = (Enums.TicketType)trainViewModel.SelectedType;

                var ticket = TicketFactory.Create(ticketType, trainId, userId, Enums.TransportEnum.Train, price);
                ticket.SendEmail(trainId, userId, Enums.TransportEnum.Train);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index"); // zmienić na error page
        }
        #endregion

        #region create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainID,DepartureStationID,ArrivalStationID,DepartureDate,ArrivalDate,NumberOfSeats,Price")] Train train)
        {
            if (ModelState.IsValid)
            {
                db.Trains.Add(train);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(train);
        }
        #endregion

        #region edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train train = db.Trains.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            return View(train);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainID,DepartureStationID,ArrivalStationID,DepartureDate,ArrivalDate,NumberOfSeats,Price")] Train train)
        {
            if (ModelState.IsValid)
            {
                db.Entry(train).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(train);
        }
        #endregion

        #region delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train train = db.Trains.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            return View(train);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Train train = db.Trains.Find(id);
            db.Trains.Remove(train);
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


            return ((StrategyContext)Session["Strategy"]).ExecuteStrategy("Trains");//sc.ExecuteStrategy("Home");
        }

        public ActionResult TilesDisplay()
        {

            StrategyContext sc = new StrategyContext(new StrategyTiles());
            Session["Strategy"] = sc;

            return ((StrategyContext)Session["Strategy"]).ExecuteStrategy("Trains");//sc.ExecuteStrategy("Home");
        }

        public ActionResult ListDisplay()
        {

            StrategyContext sc = new StrategyContext(new StrategyList());
            Session["Strategy"] = sc;

            return ((StrategyContext)Session["Strategy"]).ExecuteStrategy("Trains");//sc.ExecuteStrategy("Home");
        }


        public ActionResult GenerateSearchModel(SearchModel model)
        {
            // generate list of locations without duplicates
            List<string> ls = db.Stations.Select(p => p.Location).Distinct().ToList();
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
