namespace ZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        AirportID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        PhotoPath = c.String(),
                    })
                .PrimaryKey(t => t.AirportID);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightID = c.Int(nullable: false, identity: true),
                        DepartureAirportID = c.Int(nullable: false),
                        ArrivalAirportID = c.Int(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        ArrivalDate = c.DateTime(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FlightID)
                .ForeignKey("dbo.Airports", t => t.ArrivalAirportID)
                .ForeignKey("dbo.Airports", t => t.DepartureAirportID)
                .Index(t => t.DepartureAirportID)
                .Index(t => t.ArrivalAirportID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        FlightID = c.Int(nullable: false),
                        TrainID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Price = c.Double(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Flights", t => t.FlightID, cascadeDelete: true)
                .ForeignKey("dbo.Trains", t => t.TrainID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserID)
                .Index(t => t.FlightID)
                .Index(t => t.TrainID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        TrainID = c.Int(nullable: false, identity: true),
                        DepartureStationID = c.Int(nullable: false),
                        ArrivalStationID = c.Int(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        ArrivalDate = c.DateTime(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TrainID)
                .ForeignKey("dbo.Stations", t => t.ArrivalStationID)
                .ForeignKey("dbo.Stations", t => t.DepartureStationID)
                .Index(t => t.DepartureStationID)
                .Index(t => t.ArrivalStationID);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        StationID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        PhotoPath = c.String(),
                    })
                .PrimaryKey(t => t.StationID);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AvailableFunds = c.Double(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SearchModels",
                c => new
                    {
                        SearchModelID = c.Int(nullable: false, identity: true),
                        From = c.String(),
                        To = c.String(),
                    })
                .PrimaryKey(t => t.SearchModelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Tickets", "UserID", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Tickets", "TrainID", "dbo.Trains");
            DropForeignKey("dbo.Trains", "DepartureStationID", "dbo.Stations");
            DropForeignKey("dbo.Trains", "ArrivalStationID", "dbo.Stations");
            DropForeignKey("dbo.Tickets", "FlightID", "dbo.Flights");
            DropForeignKey("dbo.Flights", "DepartureAirportID", "dbo.Airports");
            DropForeignKey("dbo.Flights", "ArrivalAirportID", "dbo.Airports");
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Trains", new[] { "ArrivalStationID" });
            DropIndex("dbo.Trains", new[] { "DepartureStationID" });
            DropIndex("dbo.Tickets", new[] { "UserID" });
            DropIndex("dbo.Tickets", new[] { "TrainID" });
            DropIndex("dbo.Tickets", new[] { "FlightID" });
            DropIndex("dbo.Flights", new[] { "ArrivalAirportID" });
            DropIndex("dbo.Flights", new[] { "DepartureAirportID" });
            DropTable("dbo.SearchModels");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Stations");
            DropTable("dbo.Trains");
            DropTable("dbo.Tickets");
            DropTable("dbo.Flights");
            DropTable("dbo.Airports");
        }
    }
}
