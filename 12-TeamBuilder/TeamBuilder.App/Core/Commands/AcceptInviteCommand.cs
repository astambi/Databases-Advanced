namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilites;

    class AcceptInviteCommand
    {
        // AcceptInvite <teamName>
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
            AcceptInvitation(teamName);

            return $"User {currentUser.Username} joined team {teamName}!";
        }

        private void AcceptInvitation(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                // Get current user & attach to context !
                User currentUser = AuthenticationManager.GetCurrentUser();
                User invitedUser = context.Users.FirstOrDefault(u => u.Id == currentUser.Id);
                //context.Users.Attach(currentUser); // OR attach current user and use it in place of invitedUser

                // Add user to team
                invitedUser.Teams.Add(team);

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
