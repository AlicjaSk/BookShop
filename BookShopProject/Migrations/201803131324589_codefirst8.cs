namespace BookShopProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codefirst8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "NumberInWarehouse", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "NumberInWarehouse");
        }
    }
}
