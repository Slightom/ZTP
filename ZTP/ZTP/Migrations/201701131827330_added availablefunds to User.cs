namespace ZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedavailablefundstoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "AvailableFunds", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "AvailableFunds");
        }
    }
}
