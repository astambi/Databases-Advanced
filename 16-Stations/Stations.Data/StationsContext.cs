namespace Stations.Data
{
    using Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Linq;

    public class StationsContext : DbContext
    {
        public StationsContext()
            : base("name=StationsContext")
        {
        }

        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<SeatingClass> SeatingClasses { get; set; }
        public virtual DbSet<TrainSeat> TrainSeats { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<CustomerCard> CustomerCards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Station
            modelBuilder.Entity<Station>()
                .Property(s => s.Name)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Station_Name", 1) { IsUnique = true }));

            // Train
            modelBuilder.Entity<Train>()
                .Property(t => t.TrainNumber)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Train_TrainNumber") { IsUnique = true }));

            // Seating Class
            modelBuilder.Entity<SeatingClass>()
                .Property(sc => sc.Name)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_SeatingClass_Name", 1) { IsUnique = true }));

            modelBuilder.Entity<SeatingClass>()
                .Property(sc => sc.Abbreviation)
                .IsFixedLength()
                .HasMaxLength(2);

            // Trip
            modelBuilder.Entity<Trip>()
                .HasRequired(t => t.OriginStation)
                .WithMany(s => s.OriginatingTrips)
                .WillCascadeOnDelete(false)
                ;

            modelBuilder.Entity<Trip>()
                .HasRequired(t => t.DestinationStation)
                .WithMany(s => s.DestinationTrips)
                //.WillCascadeOnDelete(false)
                ;

            modelBuilder.Entity<Trip>()
                .Property(t => t.Status)
                .HasColumnAnnotation("Default", StatusType.OnTime);

            // Ticket
            modelBuilder.Entity<CustomerCard>()
                .Property(t => t.CardType)
                .HasColumnAnnotation("Default", CardType.Normal);

            base.OnModelCreating(modelBuilder);
        }
    }
}