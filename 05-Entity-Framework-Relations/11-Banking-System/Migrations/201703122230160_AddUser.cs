namespace _11_Banking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CheckingAccounts", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.SavingAccounts", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.CheckingAccounts", "UserId");
            CreateIndex("dbo.SavingAccounts", "UserId");
            AddForeignKey("dbo.CheckingAccounts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SavingAccounts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingAccounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.CheckingAccounts", "UserId", "dbo.Users");
            DropIndex("dbo.SavingAccounts", new[] { "UserId" });
            DropIndex("dbo.CheckingAccounts", new[] { "UserId" });
            DropColumn("dbo.SavingAccounts", "UserId");
            DropColumn("dbo.CheckingAccounts", "UserId");
            DropTable("dbo.Users");
        }
    }
}
