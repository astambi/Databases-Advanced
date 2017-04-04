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
            this.Property(t => t.Name)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_Teams_Name", 1) { IsUnique = true }))
                .HasMaxLength(25);

            this.Property(t => t.Description)
                .HasMaxLength(30);

            this.Property(t => t.Acronym)
                .IsRequired();

            this.HasMany(t => t.Members)
                .WithMany(u => u.Teams)                 // many-to-many User-Team
                .Map(ca =>
                {
                    ca.MapLeftKey("UserId");
                    ca.MapRightKey("TeamId");
                    ca.ToTable("UserTeams");
                });

            this.HasMany(t => t.Events)
                .WithMany(e => e.ParticipatingTeams)    // many-to-many Event-Team
                .Map(ca =>
                {
                    ca.MapLeftKey("EventId");
                    ca.MapRightKey("TeamId");
                    ca.ToTable("EventTeams");
                });

            this.HasMany(t => t.Invitations)            
                .WithRequired(i => i.Team)
                .WillCascadeOnDelete(false);

        }
    }
}
