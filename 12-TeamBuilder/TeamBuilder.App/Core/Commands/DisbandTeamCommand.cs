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

                // TODO

                Team deletedTeam = context.Teams.FirstOrDefault(t => t.Name == "DeletedTeam");
                if (deletedTeam == null)
                {
                    context.Teams.Add(new Team() { Name = "DeletedTeam", Acronym = "DEL" });
                    context.SaveChanges();
                    deletedTeam = context.Teams.FirstOrDefault(t => t.Name == "DeletedTeam");
                }

                // Move invitations to team to a deletedTeam
                var invitations = team.Invitations;
                foreach (var invite in invitations)
                {
                    invite.TeamId = deletedTeam.Id;
                }
                context.SaveChanges();

                // Remove team members
                team.Members.ToList()
                    .ForEach(m => team.Members.Remove(m));
                context.SaveChanges();

                // Delete team events
                
                // Remove team
                context.Teams.Remove(team);
                context.SaveChanges();
            }
        }
    }
}
