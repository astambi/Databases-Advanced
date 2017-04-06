namespace TeamBuilder.Data.Configurations
{
    using Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            // Event properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Events_Name", 1)
                        { IsUnique = true }))
                .HasMaxLength(25);

            this.Property(e => e.Description)
                .IsUnicode()
                .HasMaxLength(250);

            // Navigation properties
            this.HasRequired(e => e.Creator)            // one-to-many User-CreateTeam
                .WithMany(u => u.CreatedEvents);

            this.HasMany(e => e.ParticipatingTeams)     // many-to-many Event-ParticipatingTeam
                .WithMany(t => t.AttendedEvents)
                .Map(et =>
                {
                    et.MapLeftKey("EventId");
                    et.MapRightKey("TeamId");
                    et.ToTable("EventTeams");
                });
        }
    }
}
