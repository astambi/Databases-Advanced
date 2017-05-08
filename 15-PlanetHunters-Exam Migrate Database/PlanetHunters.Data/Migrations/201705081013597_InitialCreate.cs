namespace PlanetHunters.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Astronomers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Discoveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateMade = c.DateTime(nullable: false, storeType: "date"),
                        TelescopeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Telescopes", t => t.TelescopeId, cascadeDelete: true)
                .Index(t => t.TelescopeId);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Mass = c.Single(nullable: false),
                        HostStarSystemId = c.Int(nullable: false),
                        Discovery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StarSystems", t => t.HostStarSystemId, cascadeDelete: true)
                .ForeignKey("dbo.Discoveries", t => t.Discovery_Id)
                .Index(t => t.HostStarSystemId)
                .Index(t => t.Discovery_Id);
            
            CreateTable(
                "dbo.StarSystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Temperature = c.Int(nullable: false),
                        HostStarSystemId = c.Int(nullable: false),
                        Discovery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StarSystems", t => t.HostStarSystemId, cascadeDelete: true)
                .ForeignKey("dbo.Discoveries", t => t.Discovery_Id)
                .Index(t => t.HostStarSystemId)
                .Index(t => t.Discovery_Id);
            
            CreateTable(
                "dbo.Telescopes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Location = c.String(nullable: false, maxLength: 255),
                        MirrorDiameter = c.Single(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PioneersDiscoveries",
                c => new
                    {
                        PioneerId = c.Int(nullable: false),
                        DiscoveryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PioneerId, t.DiscoveryId })
                .ForeignKey("dbo.Astronomers", t => t.PioneerId, cascadeDelete: true)
                .ForeignKey("dbo.Discoveries", t => t.DiscoveryId, cascadeDelete: true)
                .Index(t => t.PioneerId)
                .Index(t => t.DiscoveryId);
            
            CreateTable(
                "dbo.ObserversDiscoveries",
                c => new
                    {
                        ObserverId = c.Int(nullable: false),
                        DiscoveryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ObserverId, t.DiscoveryId })
                .ForeignKey("dbo.Astronomers", t => t.ObserverId, cascadeDelete: true)
                .ForeignKey("dbo.Discoveries", t => t.DiscoveryId, cascadeDelete: true)
                .Index(t => t.ObserverId)
                .Index(t => t.DiscoveryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ObserversDiscoveries", "DiscoveryId", "dbo.Discoveries");
            DropForeignKey("dbo.ObserversDiscoveries", "ObserverId", "dbo.Astronomers");
            DropForeignKey("dbo.PioneersDiscoveries", "DiscoveryId", "dbo.Discoveries");
            DropForeignKey("dbo.PioneersDiscoveries", "PioneerId", "dbo.Astronomers");
            DropForeignKey("dbo.Discoveries", "TelescopeId", "dbo.Telescopes");
            DropForeignKey("dbo.Stars", "Discovery_Id", "dbo.Discoveries");
            DropForeignKey("dbo.Planets", "Discovery_Id", "dbo.Discoveries");
            DropForeignKey("dbo.Planets", "HostStarSystemId", "dbo.StarSystems");
            DropForeignKey("dbo.Stars", "HostStarSystemId", "dbo.StarSystems");
            DropIndex("dbo.ObserversDiscoveries", new[] { "DiscoveryId" });
            DropIndex("dbo.ObserversDiscoveries", new[] { "ObserverId" });
            DropIndex("dbo.PioneersDiscoveries", new[] { "DiscoveryId" });
            DropIndex("dbo.PioneersDiscoveries", new[] { "PioneerId" });
            DropIndex("dbo.Stars", new[] { "Discovery_Id" });
            DropIndex("dbo.Stars", new[] { "HostStarSystemId" });
            DropIndex("dbo.Planets", new[] { "Discovery_Id" });
            DropIndex("dbo.Planets", new[] { "HostStarSystemId" });
            DropIndex("dbo.Discoveries", new[] { "TelescopeId" });
            DropTable("dbo.ObserversDiscoveries");
            DropTable("dbo.PioneersDiscoveries");
            DropTable("dbo.Telescopes");
            DropTable("dbo.Stars");
            DropTable("dbo.StarSystems");
            DropTable("dbo.Planets");
            DropTable("dbo.Discoveries");
            DropTable("dbo.Astronomers");
        }
    }
}
