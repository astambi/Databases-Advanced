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
            // User properties
            this.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Users_Username", 1)
                        { IsUnique = true }));          // unique

            this.Property(u => u.FirstName)
                .HasMaxLength(25);

            this.Property(u => u.LastName)
                .HasMaxLength(25);

            this.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(30);

            // Navigation properties
            this.HasMany(u => u.CreatedTeams)           // one-to many User-CreatedTeam
                .WithRequired(t => t.Creator)
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.CreatedEvents)          // one-to-many User-CreatedEvent
                .WithRequired(e => e.Creator)
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.ReceivedInvitations)    // one-to-many User-ReceivedInvitation
                .WithRequired(i => i.InvitedUser)
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.Teams)                  // many-to-many User-Team
                .WithMany(t => t.Members)
                .Map(ut =>
                {
                    ut.MapLeftKey("UserId");
                    ut.MapRightKey("TeamId");
                    ut.ToTable("UserTeams");
                });
        }
    }
}
