namespace Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProducts", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProducts", "Quantity");
        }
    }
}
