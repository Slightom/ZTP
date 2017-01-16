namespace ZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tickeTest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "FlightID", "dbo.Flights");
            DropForeignKey("dbo.Tickets", "TrainID", "dbo.Trains");
            DropIndex("dbo.Tickets", new[] { "FlightID" });
            DropIndex("dbo.Tickets", new[] { "TrainID" });
            AlterColumn("dbo.Tickets", "FlightID", c => c.Int());
            AlterColumn("dbo.Tickets", "TrainID", c => c.Int());
            CreateIndex("dbo.Tickets", "FlightID");
            CreateIndex("dbo.Tickets", "TrainID");
            AddForeignKey("dbo.Tickets", "FlightID", "dbo.Flights", "FlightID");
            AddForeignKey("dbo.Tickets", "TrainID", "dbo.Trains", "TrainID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TrainID", "dbo.Trains");
            DropForeignKey("dbo.Tickets", "FlightID", "dbo.Flights");
            DropIndex("dbo.Tickets", new[] { "TrainID" });
            DropIndex("dbo.Tickets", new[] { "FlightID" });
            AlterColumn("dbo.Tickets", "TrainID", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "FlightID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TrainID");
            CreateIndex("dbo.Tickets", "FlightID");
            AddForeignKey("dbo.Tickets", "TrainID", "dbo.Trains", "TrainID", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "FlightID", "dbo.Flights", "FlightID", cascadeDelete: true);
        }
    }
}
