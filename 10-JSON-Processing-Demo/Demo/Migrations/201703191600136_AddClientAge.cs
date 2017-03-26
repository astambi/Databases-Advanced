namespace Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Age", c => c.Int(nullable: false, defaultValue: 18));
            Sql("UPDATE CLIENTS SET AGE = 18");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Age");
        }
    }
}
