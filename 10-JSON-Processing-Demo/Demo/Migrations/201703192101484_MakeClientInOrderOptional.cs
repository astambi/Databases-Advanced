namespace Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeClientInOrderOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "ClientId" });
            AlterColumn("dbo.Orders", "ClientId", c => c.Int());
            CreateIndex("dbo.Orders", "ClientId");
            AddForeignKey("dbo.Orders", "ClientId", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "ClientId" });
            AlterColumn("dbo.Orders", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ClientId");
            AddForeignKey("dbo.Orders", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}
