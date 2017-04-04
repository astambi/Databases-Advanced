namespace TeamBuilder.Data.Configurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    class InvitationConfiguration : EntityTypeConfiguration<Invitation>
    {
        public InvitationConfiguration()
        {
            this.HasRequired(i => i.InvitedUser)
                .WithMany(u => u.ReceivedInvitations)   // one-to-many User-Invitation
                .WillCascadeOnDelete(false);

            this.HasRequired(i => i.Team)
                .WithMany(t => t.Invitations)           // one-to-many Team-Invitation
                .WillCascadeOnDelete(false);
        }
    }
}
