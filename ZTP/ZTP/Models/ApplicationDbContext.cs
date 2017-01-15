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



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Messages relations
            modelBuilder.Entity<Flight>()
                        .HasRequired(b => b.ArrivalAirport)
                        .WithMany(a => a.FlightsFrom)
                        .HasForeignKey(b => b.ArrivalAirportID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                        .HasRequired(b => b.DepartureAirport)
                        .WithMany(a => a.FlightsTo)
                        .HasForeignKey(b => b.DepartureAirportID)
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