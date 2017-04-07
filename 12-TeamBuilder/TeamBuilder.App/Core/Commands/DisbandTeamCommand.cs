namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilites;

    class DisbandTeamCommand
    {
        // Disband <teamName>
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(1, inputArgs);

            // Authorize logged in user
            AuthenticationManager.Authorize();
            User currentUser = AuthenticationManager.GetCurrentUser();

            // Get Arguments
            string teamName = inputArgs[0];

            // Validate team
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            // Check is current user is team creator
            if (!CommandHelper.IsUserCreatorOfTeam(teamName, currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            // Delete team
            DeleteTeam(teamName);

            return $"{teamName} has disbanded!";
        }

        private void DeleteTeam(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                // Delete all team invitations (active or inactive)
                context.Invitations.RemoveRange(team.Invitations);
                context.SaveChanges();

                // Remove all team members from team
                var members = team.Members;
                foreach (User member in members)
                {
                    member.Teams.Remove(team);
                }
                context.SaveChanges();

                // Delete team
                context.Teams.Remove(team);
                context.SaveChanges();
            }
        }
    }
}
