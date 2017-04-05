namespace TeamBuilder.Data.Configurations
{
    using Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            // Team properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Teams_Name", 1)
                    { IsUnique = true }));

            this.Property(t => t.Description)
                .HasMaxLength(32);

            this.Property(t => t.Acrinym)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Navigation properties
            this.HasMany(t => t.Members)            // many-to-many User-Team
                .WithMany(u => u.Teams)
                .Map(ut =>
                {
                    ut.MapLeftKey("UserId");
                    ut.MapRightKey("TeamId");
                    ut.ToTable("UserTeams");
                });

            this.HasRequired(t => t.Creator)        // one-to-many User-CreatedTeam
                .WithMany(u => u.CreatedTeams);

            this.HasMany(t => t.Invitations)        // one-to-many Team-Invitation
                .WithRequired(i => i.Team)
                .WillCascadeOnDelete(false);

            this.HasMany(t => t.AttendedEvents)     // many-to-many Event-ParticipatingTeam
                .WithMany(e => e.ParticipatingTeams)
                .Map(et =>
                {
                    et.MapLeftKey("EventId");
                    et.MapRightKey("TeamId");
                    et.ToTable("EventTeams");
                });
        }
    }
}
