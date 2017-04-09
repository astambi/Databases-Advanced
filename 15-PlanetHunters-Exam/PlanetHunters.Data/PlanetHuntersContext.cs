namespace PlanetHunters.Data
{
    using Models;
    using System.Data.Entity;

    public class PlanetHuntersContext : DbContext
    {

        public PlanetHuntersContext()
            : base("name=PlanetHuntersContext")
        {
        }

        public virtual DbSet<Astronomer> Astronomers { get; set; }
        public virtual DbSet<Discovery> Discoveries { get; set; }
        public virtual DbSet<Telescope> Telescopes { get; set; }
        public virtual DbSet<StarSystem> StarSystems { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Many-to-many Pioners-DiscoveriesMade
            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.DiscoveriesMade)
                .WithMany(d => d.Pioneers)
                .Map(ad =>
                {
                    ad.MapLeftKey("PioneerId");
                    ad.MapRightKey("DiscoveryId");
                    ad.ToTable("PioneersDiscoveries");
                });

            // Many-to-Many Observers-DiscoveriesObserved
            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.DiscoveryObservations)
                .WithMany(d => d.Observers)
                .Map(ad =>
                {
                    ad.MapLeftKey("ObserverId");
                    ad.MapRightKey("DiscoveryId");
                    ad.ToTable("ObserversDiscoveries");
                });

            modelBuilder.Entity<Star>()
                .HasRequired(s => s.HostStarSystem)
                .WithMany(ss => ss.Stars);

            modelBuilder.Entity<Planet>()
                .HasRequired(s => s.HostStarSystem)
                .WithMany(ss => ss.Planets);        
                
            base.OnModelCreating(modelBuilder);
        }
    }
}