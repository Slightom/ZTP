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
            a.Location = "Poland, Warsaw";
            a.Name = "Warsaw Chopin Airport";
            a.PhotoPath = "/Content/photos/warsaw.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Poland, Warsaw";
            a.Name = "Warsaw-Modlin Airport";
            a.PhotoPath = "/Content/photos/warsaw.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Poland, Radom";
            a.Name = "Radom Airport";
            a.PhotoPath = "/Content/photos/radom.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "United Kingdom, London";
            a.Name = "London Heathrow Airport";
            a.PhotoPath = "/Content/photos/london.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Germany, Frankfurt";
            a.Name = "Frankfurt Airport";
            a.PhotoPath = "/Content/photos/frankfurt.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "USA, Los Angeles";
            a.Name = "Los Angeles Airport";
            a.PhotoPath = "/Content/photos/losangeles.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "USA, Denver";
            a.Name = "Denver International Airport";
            a.PhotoPath = "/Content/photos/denver.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "Spain, Madrit";
            a.Name = "Madrid Barajas Airport";
            a.PhotoPath = "/Content/photos/madrit.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Airport();
            a.Location = "France, Paris";
            a.Name = "Paris-Orly Airport";
            a.PhotoPath = "/Content/photos/paris.png";
            context.Airports.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();





            //////////////////////////////////////////////////////////////////////////////////////////
            Flight f = new Flight();
            int departureairportid = context.Airports.Where(p => p.Name == "Warsaw Chopin Airport").Select(p=>p.AirportID).First();
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
        }
    }
}
