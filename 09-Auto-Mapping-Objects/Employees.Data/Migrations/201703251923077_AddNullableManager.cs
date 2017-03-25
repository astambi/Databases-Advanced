namespace Employees.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableManager : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            AlterColumn("dbo.Employees", "ManagerId", c => c.Int());
            CreateIndex("dbo.Employees", "ManagerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            AlterColumn("dbo.Employees", "ManagerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "ManagerId");
        }
    }
}
