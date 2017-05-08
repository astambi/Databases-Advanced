namespace PlanetHunters.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJournalsPublications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.DateTime(nullable: false, storeType: "date"),
                        DiscoveryId = c.Int(nullable: false),
                        JournalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discoveries", t => t.DiscoveryId, cascadeDelete: true)
                .ForeignKey("dbo.Journals", t => t.JournalId, cascadeDelete: true)
                .Index(t => t.DiscoveryId)
                .Index(t => t.JournalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publications", "JournalId", "dbo.Journals");
            DropForeignKey("dbo.Publications", "DiscoveryId", "dbo.Discoveries");
            DropIndex("dbo.Publications", new[] { "JournalId" });
            DropIndex("dbo.Publications", new[] { "DiscoveryId" });
            DropTable("dbo.Publications");
            DropTable("dbo.Journals");
        }
    }
}
