namespace BookShopProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codefirst5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Street", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "NumberStreet", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Surname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Surname", c => c.String());
            AlterColumn("dbo.Clients", "Name", c => c.String());
            DropColumn("dbo.Clients", "City");
            DropColumn("dbo.Clients", "NumberStreet");
            DropColumn("dbo.Clients", "Street");
        }
    }
}
