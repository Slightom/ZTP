namespace ZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedatefromsearchmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SearchModels", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SearchModels", "Date", c => c.DateTime(nullable: false));
        }
    }
}
