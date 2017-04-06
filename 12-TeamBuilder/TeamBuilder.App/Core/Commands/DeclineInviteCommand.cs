namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilites;

    class DeclineInviteCommand
    {
        // DeclineInvite <teamName>
        public string Execute(string[] inputArgs)
        {
            // Validate input arguments count
            Check.CheckLength(1, inputArgs);

            // Authorize user
            AuthenticationManager.Authorize();
            User currentUser = AuthenticationManager.GetCurrentUser();

            // Get arguments
            string teamName = inputArgs[0];

            // Validate team
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            // Validate invitation
            if (!CommandHelper.IsInviteExisting(teamName, currentUser))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
            }

            // Accept invitation
            DeclineInvitation(teamName);

            return $"Invite from {teamName} declined.";
        }

        private void DeclineInvitation(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User invitedUser = AuthenticationManager.GetCurrentUser();
                
                // Make invitation inactive
                context.Invitations
                    .FirstOrDefault(i => i.Team.Name == teamName &&
                                         i.InvitedUser.Id == invitedUser.Id &&
                                         i.IsActive)
                    .IsActive = false;

                context.SaveChanges();
            }
        }
    }
}
