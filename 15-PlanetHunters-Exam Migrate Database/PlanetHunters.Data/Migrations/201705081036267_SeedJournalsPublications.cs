namespace PlanetHunters.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class SeedJournalsPublications : DbMigration
    {
        public override void Up()
        {
            SeedJournals();
            SeedPublications();
        }

        public override void Down()
        {
            RestoreDiscoveriesDateMade();
        }

        private static void RestoreDiscoveriesDateMade()
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var discovery in context.Discoveries)
                {
                    discovery.DateMade = context.Publications.FirstOrDefault(p => p.DiscoveryId == discovery.Id).ReleaseDate;
                }
                context.SaveChanges();
            }
        }

        private void SeedPublications()
        {
            using (var context = new PlanetHuntersContext())
            {
                var discoveries = context.Discoveries;
                foreach (var discovery in discoveries)
                {
                    context.Publications.AddOrUpdate(p => new { p.ReleaseDate, p.DiscoveryId },
                        new Publication
                        {
                            JournalId = context.Journals.FirstOrDefault(j => j.Name == "Astronomy").Id,
                            ReleaseDate = discovery.DateMade,
                            DiscoveryId = discovery.Id
                        });
                }
                context.SaveChanges();
            }
        }

        private void SeedJournals()
        {
            using (var context = new PlanetHuntersContext())
            {
                context.Journals.AddOrUpdate(j => j.Name,
                new Journal { Name = "Astronomy" },
                new Journal { Name = "New Astronomy" },
                new Journal { Name = "Astronomy and Astrophysics" });
                context.SaveChanges();
            }
        }
    }
}
