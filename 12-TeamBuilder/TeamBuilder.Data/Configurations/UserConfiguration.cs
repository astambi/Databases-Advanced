namespace TeamBuilder.Data.Configurations
{
    using Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.Property(u => u.Username)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Users_Username", 1) { IsUnique = true }))                // Unique
                .HasMaxLength(25);

            this.Property(u => u.FirstName)
                .HasMaxLength(25);

            this.Property(u => u.LastName)
                .HasMaxLength(25);

            this.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(30);

            this.HasMany(u => u.CreatedTeams)
                .WithRequired(t => t.Creator)   // one-to-many User-Team
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.CreatedEvents)
                .WithRequired(e => e.Creator)   // one-to-many User-Event
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.Teams)
                .WithMany(t => t.Members)       // many-to-many User-Team
                .Map(ca =>
                {
                    ca.MapLeftKey("UserId");
                    ca.MapRightKey("TeamId");
                    ca.ToTable("UserTeams");
                });

            this.HasMany(u => u.ReceivedInvitations)
                .WithRequired(i => i.InvitedUser)
                .WillCascadeOnDelete(false);
            
        }
    }
}
