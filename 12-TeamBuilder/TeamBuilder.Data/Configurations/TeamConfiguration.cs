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
                    new IndexAnnotation(
                        new IndexAttribute("IX_Teams_Name", 1)
                        { IsUnique = true }));      // unique

            this.Property(t => t.Description)
                .HasMaxLength(32);

            this.Property(t => t.Acronym)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Navigation properties
            this.HasMany(t => t.Invitations)        // one-to-many Team-Invitation
                .WithRequired(i => i.Team)
                .WillCascadeOnDelete(false);  
        }
    }
}
