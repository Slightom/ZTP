namespace ZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPhotoPathtoAirport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Airports", "PhotoPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Airports", "PhotoPath");
        }
    }
}
