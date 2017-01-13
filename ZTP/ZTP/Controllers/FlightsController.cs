using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZTP.Models;
using ZTP.Models.Strategy;

namespace ZTP.Controllers
{
    public class FlightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flights
        public ActionResult Index()
        {
            var flights = db.Flights.Include(f => f.ArrivalAirport).Include(f => f.DepartureAirport);
            return View(flights.ToList());
        }


        // GET: Flights/Details/5
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
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            ViewBag.ArrivalAirportID = new SelectList(db.Airports, "AirportID", "Name");
            ViewBag.DepartureAirportID = new SelectList(db.Airports, "AirportID", "Name");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Flights/Edit/5
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

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Flights/Delete/5
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

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
