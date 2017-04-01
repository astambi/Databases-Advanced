namespace Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Address");
        }
    }
}
