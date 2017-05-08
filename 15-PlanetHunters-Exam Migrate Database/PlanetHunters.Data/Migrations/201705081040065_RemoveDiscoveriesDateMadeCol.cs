namespace PlanetHunters.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDiscoveriesDateMadeCol : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Discoveries", "DateMade");
        }

        public override void Down()
        {
            AddColumn("dbo.Discoveries", "DateMade", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
