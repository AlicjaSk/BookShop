namespace BookShopProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codefirst2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            AddColumn("dbo.Books", "AuthorRefId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "AuthorRefId");
            AddForeignKey("dbo.Books", "AuthorRefId", "dbo.Authors", "AuthorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorRefId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "AuthorRefId" });
            DropColumn("dbo.Books", "AuthorRefId");
            DropTable("dbo.Authors");
        }
    }
}
