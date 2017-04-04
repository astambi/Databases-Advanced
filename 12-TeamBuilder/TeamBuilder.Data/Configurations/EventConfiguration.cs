namespace TeamBuilder.Data.Configurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            this.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(25);

            this.Property(e => e.Description)
                .IsUnicode()
                .HasMaxLength(250);
            
            this.HasRequired(e => e.Creator)
                .WithMany(c => c.CreatedEvents);

            this.HasMany(e => e.ParticipatingTeams) // many-to-many Event-Team
                .WithMany(t => t.Events)
                .Map(ca =>
                {
                    ca.MapLeftKey("EventId");
                    ca.MapRightKey("TeamId");
                    ca.ToTable("EventTeams");
                });
        }
    }
}
