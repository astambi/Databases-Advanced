namespace TeamBuilder.Data.Configurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    class InvitationConfiguration : EntityTypeConfiguration<Invitation>
    {
        public InvitationConfiguration()
        {
            this.HasRequired(i => i.InvitedUser)        // one-to-many user-receivedinvitation
                .WithMany(u => u.ReceivedInvitations);

            this.HasRequired(i => i.Team)               // one-to-many Team-Invitation
                .WithMany(t => t.Invitations);
        }
    }
}
