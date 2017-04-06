namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilites;

    class KickMemberCommand
    {
        // KickMember <teamName> <username>
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(2, inputArgs);

            // Authorize user
            AuthenticationManager.Authorize();
            User currentUser = AuthenticationManager.GetCurrentUser();

            // Get Arguments
            string teamName = inputArgs[0];
            string username = inputArgs[1];

            // Validate team
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            // Validate user
            if (!CommandHelper.IsUserExisting(username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, username));
            }

            // Check if user is a team member
            if (!CommandHelper.IsMemberOfTeam(teamName, username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.NotPartOfTeam, username, teamName));
            }

            // Check if current user is team creator
            if (!CommandHelper.IsUserCreatorOfTeam(teamName, currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            // Check if user to be kicked is team creator (by now current user is team creator)
            if (username == currentUser.Username)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.CommandNotAllowed, "DisbandTeam"));
            }

            // Remove user from team
            RemoveUserFromTeam(username, teamName);

            return $"User {username} was kicked from {teamName}!";
        }

        private void RemoveUserFromTeam(string username, string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Username == username);
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                user.Teams.Remove(team);
                context.SaveChanges();
            }
        }
    }
}
