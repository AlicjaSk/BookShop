namespace BookShopProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codefirst4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        ClientRefId = c.Int(nullable: false),
                        BookRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.Books", t => t.BookRefId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientRefId, cascadeDelete: true)
                .Index(t => t.ClientRefId)
                .Index(t => t.BookRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "ClientRefId", "dbo.Clients");
            DropForeignKey("dbo.Purchases", "BookRefId", "dbo.Books");
            DropIndex("dbo.Purchases", new[] { "BookRefId" });
            DropIndex("dbo.Purchases", new[] { "ClientRefId" });
            DropTable("dbo.Purchases");
        }
    }
}
