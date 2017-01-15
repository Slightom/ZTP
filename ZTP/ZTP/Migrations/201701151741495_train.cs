namespace ZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class train : DbMigration
    {
        public override void Up()
        {
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
                        Station_StationID = c.Int(),
                        Station_StationID1 = c.Int(),
                        ArrivalStation_StationID = c.Int(),
                        DepartureStation_StationID = c.Int(),
                    })
                .PrimaryKey(t => t.TrainID)
                .ForeignKey("dbo.Stations", t => t.Station_StationID)
                .ForeignKey("dbo.Stations", t => t.Station_StationID1)
                .ForeignKey("dbo.Stations", t => t.ArrivalStation_StationID)
                .ForeignKey("dbo.Stations", t => t.DepartureStation_StationID)
                .Index(t => t.Station_StationID)
                .Index(t => t.Station_StationID1)
                .Index(t => t.ArrivalStation_StationID)
                .Index(t => t.DepartureStation_StationID);
            
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
            
            AddColumn("dbo.Tickets", "TrainID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TrainID");
            AddForeignKey("dbo.Tickets", "TrainID", "dbo.Trains", "TrainID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TrainID", "dbo.Trains");
            DropForeignKey("dbo.Trains", "DepartureStation_StationID", "dbo.Stations");
            DropForeignKey("dbo.Trains", "ArrivalStation_StationID", "dbo.Stations");
            DropForeignKey("dbo.Trains", "Station_StationID1", "dbo.Stations");
            DropForeignKey("dbo.Trains", "Station_StationID", "dbo.Stations");
            DropIndex("dbo.Trains", new[] { "DepartureStation_StationID" });
            DropIndex("dbo.Trains", new[] { "ArrivalStation_StationID" });
            DropIndex("dbo.Trains", new[] { "Station_StationID1" });
            DropIndex("dbo.Trains", new[] { "Station_StationID" });
            DropIndex("dbo.Tickets", new[] { "TrainID" });
            DropColumn("dbo.Tickets", "TrainID");
            DropTable("dbo.Stations");
            DropTable("dbo.Trains");
        }
    }
}
