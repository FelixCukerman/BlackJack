namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Suit = c.Int(nullable: false),
                        Key = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nickname = c.String(nullable: false),
                        UserRole = c.Int(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCards",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Card_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Card_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cards", t => t.Card_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Card_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCards", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.UserCards", "User_Id", "dbo.Users");
            DropIndex("dbo.UserCards", new[] { "Card_Id" });
            DropIndex("dbo.UserCards", new[] { "User_Id" });
            DropTable("dbo.UserCards");
            DropTable("dbo.Users");
            DropTable("dbo.Cards");
        }
    }
}
