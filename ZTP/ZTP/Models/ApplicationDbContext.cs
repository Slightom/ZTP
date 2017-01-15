using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZTP.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("myCS", throwIfV1Schema: false)
        {
        }



        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Station> Stations { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // 1 Flight 2 Airports
            modelBuilder.Entity<Flight>()
                        .HasRequired(b => b.ArrivalAirport)
                        .WithMany(a => a.FlightsTo)
                        .HasForeignKey(b => b.ArrivalAirportID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                        .HasRequired(b => b.DepartureAirport)
                        .WithMany(a => a.FlightsFrom)
                        .HasForeignKey(b => b.DepartureAirportID)
                        .WillCascadeOnDelete(false);

            // 1 Train 2 Stations
            modelBuilder.Entity<Train>()
                        .HasRequired(b => b.ArrivalStation)
                        .WithMany(a => a.TrainsTo)
                        .HasForeignKey(b => b.ArrivalStationID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Train>()
                        .HasRequired(b => b.DepartureStation)
                        .WithMany(a => a.TrainsFrom)
                        .HasForeignKey(b => b.DepartureStationID)
                        .WillCascadeOnDelete(false);


            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }





        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ZTP.Models.SearchModel> SearchModels { get; set; }
    }
}