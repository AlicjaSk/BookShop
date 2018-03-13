namespace BookShopProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codefirst3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Cost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Cost");
        }
    }
}
