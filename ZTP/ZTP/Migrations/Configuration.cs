namespace ZTP.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ZTP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ZTP.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Models.IdentityManager im = new Models.IdentityManager();
            if (!im.RoleExists("Admin")) { im.CreateRole("Admin"); }
            if (!im.RoleExists("User")) { im.CreateRole("User"); }

            var passwordHash = new PasswordHasher();
            ApplicationUser u = new ApplicationUser();
            u.Email = "tomasz.suchwalko@gmail.com";
            u.UserName = "slightom";
            u.PasswordHash = passwordHash.HashPassword("Slightomp+");
            u.SecurityStamp = Guid.NewGuid().ToString();
            u.AvailableFunds = 1000;
            context.Users.AddOrUpdate(p => p.UserName, u);
            context.SaveChanges();
            im.AddUserToRoleByUsername("slightom", "User");

            u = new ApplicationUser();
            u.Email = "witek15@gmail.com";
            u.UserName = "witek15";
            //u.Nick = "witek15";
            u.PasswordHash = passwordHash.HashPassword("Witek15p+");
            u.SecurityStamp = Guid.NewGuid().ToString();
            u.AvailableFunds = 1000;
            context.Users.AddOrUpdate(p => p.UserName, u);
            context.SaveChanges();
            im.AddUserToRoleByUsername("witek15", "User");





            ////////////////////////////////////////////////////////////////////////////////////////
            Airport a = new Airport();
            a.Location = "Warsaw, Poland";
            a.Name = "Warsaw Chopin Airport";
            a.PhotoPath = "/Content/photos/warsaw.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Warsaw, Poland";
            a.Name = "Warsaw-Modlin Airport";
            a.PhotoPath = "/Content/photos/warsaw.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Warsaw, Poland";
            a.Name = "Radom Airport";
            a.PhotoPath = "/Content/photos/radom.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "London, United Kingdom";
            a.Name = "London Heathrow Airport";
            a.PhotoPath = "/Content/photos/london.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Frankfurt, Germany";
            a.Name = "Frankfurt Airport";
            a.PhotoPath = "/Content/photos/frankfurt.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Los Angeles, USA";
            a.Name = "Los Angeles Airport";
            a.PhotoPath = "/Content/photos/losangeles.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Denver, USA";
            a.Name = "Denver International Airport";
            a.PhotoPath = "/Content/photos/denver.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Madrit, Spain";
            a.Name = "Madrid Barajas Airport";
            a.PhotoPath = "/Content/photos/madrit.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Paris, France";
            a.Name = "Paris-Orly Airport";
            a.PhotoPath = "/Content/photos/paris.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();





            //////////////////////////////////////////////////////////////////////////////////////////
            Flight f = new Flight();
            int departureairportid = context.Airports.Where(p => p.Name == "Warsaw Chopin Airport").Select(p => p.AirportID).First();
            int arrivalairportid = context.Airports.Where(p => p.Name == "London Heathrow Airport").Select(p => p.AirportID).First();
            f.DepartureAirportID = departureairportid;
            f.ArrivalAirportID = arrivalairportid;
            f.DepartureDate = new DateTime(2017, 01, 20, 10, 00, 00);
            f.ArrivalDate = new DateTime(2017, 01, 20, 12, 45, 00);
            f.NumberOfSeats = 200;
            f.Price = 500;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();

            f.DepartureAirportID = arrivalairportid;
            f.ArrivalAirportID = departureairportid;
            f.DepartureDate = new DateTime(2017, 01, 22, 13, 00, 00);
            f.ArrivalDate = new DateTime(2017, 01, 22, 15, 45, 00);
            f.NumberOfSeats = 180;
            f.Price = 480;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();



            departureairportid = context.Airports.Where(p => p.Name == "Warsaw Chopin Airport").Select(p => p.AirportID).First();
            arrivalairportid = context.Airports.Where(p => p.Name == "Los Angeles Airport").Select(p => p.AirportID).First();
            f.DepartureAirportID = departureairportid;
            f.ArrivalAirportID = arrivalairportid;
            f.DepartureDate = new DateTime(2017, 01, 24, 08, 00, 00);
            f.ArrivalDate = new DateTime(2017, 01, 24, 22, 30, 00);
            f.NumberOfSeats = 300;
            f.Price = 1000;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();

            f.DepartureAirportID = arrivalairportid;
            f.ArrivalAirportID = departureairportid;
            f.DepartureDate = new DateTime(2017, 01, 25, 06, 00, 00);
            f.ArrivalDate = new DateTime(2017, 01, 25, 23, 30, 00);
            f.NumberOfSeats = 240;
            f.Price = 2500;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();



            departureairportid = context.Airports.Where(p => p.Name == "Warsaw-Modlin Airport").Select(p => p.AirportID).First();
            arrivalairportid = context.Airports.Where(p => p.Name == "Frankfurt Airport").Select(p => p.AirportID).First();
            f.DepartureAirportID = departureairportid;
            f.ArrivalAirportID = arrivalairportid;
            f.DepartureDate = new DateTime(2017, 01, 26, 08, 00, 00);
            f.ArrivalDate = new DateTime(2017, 01, 26, 10, 00, 00);
            f.NumberOfSeats = 350;
            f.Price = 500;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();

            f.DepartureAirportID = arrivalairportid;
            f.ArrivalAirportID = departureairportid;
            f.DepartureDate = new DateTime(2017, 01, 27, 11, 00, 00);
            f.ArrivalDate = new DateTime(2017, 01, 27, 13, 30, 00);
            f.NumberOfSeats = 240;
            f.Price = 650;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();




            departureairportid = context.Airports.Where(p => p.Name == "Warsaw-Modlin Airport").Select(p => p.AirportID).First();
            arrivalairportid = context.Airports.Where(p => p.Name == "Madrid Barajas Airport").Select(p => p.AirportID).First();
            f.DepartureAirportID = departureairportid;
            f.ArrivalAirportID = arrivalairportid;
            f.DepartureDate = new DateTime(2017, 01, 29, 11, 00, 00);
            f.ArrivalDate = new DateTime(2017, 01, 29, 15, 00, 00);
            f.NumberOfSeats = 400;
            f.Price = 270;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();

            f.DepartureAirportID = arrivalairportid;
            f.ArrivalAirportID = departureairportid;
            f.DepartureDate = new DateTime(2017, 01, 31, 19, 00, 00);
            f.ArrivalDate = new DateTime(2017, 01, 31, 23, 45, 00);
            f.NumberOfSeats = 380;
            f.Price = 350;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();





            departureairportid = context.Airports.Where(p => p.Name == "Warsaw-Modlin Airport").Select(p => p.AirportID).First();
            arrivalairportid = context.Airports.Where(p => p.Name == "Paris-Orly Airport").Select(p => p.AirportID).First();
            f.DepartureAirportID = departureairportid;
            f.ArrivalAirportID = arrivalairportid;
            f.DepartureDate = new DateTime(2017, 02, 02, 06, 00, 00);
            f.ArrivalDate = new DateTime(2017, 02, 02, 11, 00, 00);
            f.NumberOfSeats = 400;
            f.Price = 1000;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();

            f.DepartureAirportID = arrivalairportid;
            f.ArrivalAirportID = departureairportid;
            f.DepartureDate = new DateTime(2017, 02, 03, 15, 00, 00);
            f.ArrivalDate = new DateTime(2017, 02, 03, 22, 45, 00);
            f.NumberOfSeats = 380;
            f.Price = 1500;
            context.Flights.AddOrUpdate(p => p.DepartureDate, f);
            context.SaveChanges();




            ////////////////////////////////////////////////////////////////////////////////////////
            Station s = new Station();
            s.Name = "Railway Station, Bialystok";
            s.Location = "Bialystok, Poland";
            s.PhotoPath = "/Content/photos/bialystok.jpg";
            context.Stations.AddOrUpdate(p => p.Name, s);
            context.SaveChanges();

            s = new Station();
            s.Name = "Railway Station, Gdansk-Wrzeszcz";
            s.Location = "Gdansk, Poland";
            s.PhotoPath = "/Content/photos/gdansk.jpg";
            context.Stations.AddOrUpdate(p => p.Name, s);
            context.SaveChanges();

            s = new Station();
            s.Name = "Railway Station, Warsaw-Central";
            s.Location = "Warsaw, Poland";
            s.PhotoPath = "/Content/photos/warsaw.png";
            context.Stations.AddOrUpdate(p => p.Name, s);
            context.SaveChanges();

            s = new Station();
            s.Name = "Railway Station, Krakow-Main";
            s.Location = "Krakow, Poland";
            s.PhotoPath = "/Content/photos/krakow.jpg";
            context.Stations.AddOrUpdate(p => p.Name, s);
            context.SaveChanges();





            ////////////////////////////////////////////////////////////////////////////////////////
            Train t = new Train();
            int departurestationid = context.Stations.Where(p => p.Name == "Railway Station, Bialystok").Select(p => p.StationID).First();
            int arrivalstationid = context.Stations.Where(p => p.Name == "Railway Station, Gdansk-Wrzeszcz").Select(p => p.StationID).First();
            t.DepartureStationID = departurestationid;
            t.ArrivalStationID = arrivalstationid;
            t.DepartureDate = new DateTime(2017, 01, 20, 10, 00, 00);
            t.ArrivalDate = new DateTime(2017, 01, 20, 12, 50, 00);
            t.NumberOfSeats = 200;
            t.Price = 60;
            context.Trains.AddOrUpdate(p => p.DepartureDate, t);
            context.SaveChanges();

            t = new Train();
            departurestationid = context.Stations.Where(p => p.Name == "Railway Station, Bialystok").Select(p => p.StationID).First();
            arrivalstationid = context.Stations.Where(p => p.Name == "Railway Station, Krakow-Main").Select(p => p.StationID).First();
            t.DepartureStationID = departurestationid;
            t.ArrivalStationID = arrivalstationid;
            t.DepartureDate = new DateTime(2017, 01, 22, 12, 00, 00);
            t.ArrivalDate = new DateTime(2017, 01, 22, 18, 25, 00);
            t.NumberOfSeats = 300;
            t.Price = 100;
            context.Trains.AddOrUpdate(p => p.DepartureDate, t);
            context.SaveChanges();

            t = new Train();
            departurestationid = context.Stations.Where(p => p.Name == "Railway Station, Krakow-Main").Select(p => p.StationID).First();
            arrivalstationid = context.Stations.Where(p => p.Name == "Railway Station, Warsaw-Central").Select(p => p.StationID).First();
            t.DepartureStationID = departurestationid;
            t.ArrivalStationID = arrivalstationid;
            t.DepartureDate = new DateTime(2017, 01, 24, 06, 00, 00);
            t.ArrivalDate = new DateTime(2017, 01, 24, 09, 45, 00);
            t.NumberOfSeats = 420;
            t.Price = 80;
            context.Trains.AddOrUpdate(p => p.DepartureDate, t);
            context.SaveChanges();

            t = new Train();
            departurestationid = context.Stations.Where(p => p.Name == "Railway Station, Warsaw-Central").Select(p => p.StationID).First();
            arrivalstationid = context.Stations.Where(p => p.Name == "Railway Station, Bialystok").Select(p => p.StationID).First();
            t.DepartureStationID = departurestationid;
            t.ArrivalStationID = arrivalstationid;
            t.DepartureDate = new DateTime(2017, 01, 19, 14, 00, 00);
            t.ArrivalDate = new DateTime(2017, 01, 19, 17, 45, 00);
            t.NumberOfSeats = 500;
            t.Price = 70;
            context.Trains.AddOrUpdate(p => p.DepartureDate, t);
            context.SaveChanges();

            t = new Train();
            departurestationid = context.Stations.Where(p => p.Name == "Railway Station, Warsaw-Central").Select(p => p.StationID).First();
            arrivalstationid = context.Stations.Where(p => p.Name == "Railway Station, Gdansk-Wrzeszcz").Select(p => p.StationID).First();
            t.DepartureStationID = departurestationid;
            t.ArrivalStationID = arrivalstationid;
            t.DepartureDate = new DateTime(2017, 01, 18, 06, 00, 00);
            t.ArrivalDate = new DateTime(2017, 01, 18, 07, 45, 00);
            t.NumberOfSeats = 500;
            t.Price = 60;
            context.Trains.AddOrUpdate(p => p.DepartureDate, t);
            context.SaveChanges();
        }
    }
}
