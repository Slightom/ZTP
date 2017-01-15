namespace ZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedgenerateSearchModelmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchModels",
                c => new
                    {
                        SearchModelID = c.Int(nullable: false, identity: true),
                        From = c.String(),
                        To = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SearchModelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SearchModels");
        }
    }
}
