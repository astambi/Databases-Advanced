namespace MassDefect.Data
{
    using Models;
    using System.Data.Entity;

    public class MassDefectContext : DbContext
    {
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<MassDefectContext>());
        }

        public virtual DbSet<SolarSystem> SolarSystems { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Anomaly> Anomalies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Required Name
            //modelBuilder.Entity<SolarSystem>().Property(ss => ss.Name).IsRequired();
            //modelBuilder.Entity<Star>().Property(s => s.Name).IsRequired();
            //modelBuilder.Entity<Planet>().Property(p => p.Name).IsRequired();
            //modelBuilder.Entity<Person>().Property(p => p.Name).IsRequired();

            // Anomaly
            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.OriginPlanet)
                .WithMany(p => p.OriginatingAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.TeleportPlanet)
                .WithMany(p => p.TargettingAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
               .HasMany(a => a.Victims)         // many-to-many
               .WithMany(v => v.Anomalies)
               .Map(av =>
               {
                   av.MapLeftKey("AnomalyId");
                   av.MapRightKey("PersonId");
                   av.ToTable("AnomalyVictims");
               });

            // Planet
            modelBuilder.Entity<Planet>()
                .HasRequired(p => p.Sun)
                .WithMany(s => s.Planets)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}