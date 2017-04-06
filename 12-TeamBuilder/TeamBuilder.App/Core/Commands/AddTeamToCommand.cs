namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilites;

    class AddTeamToCommand
    {
        // AddTeamTo <eventName> <teamName>
        public string Execute(string[] inputArgs)
        {
            // Validate arguments counte
            Check.CheckLength(2, inputArgs);

            // Authorize logged in user
            AuthenticationManager.Authorize();
            User currentUser = AuthenticationManager.GetCurrentUser();

            // Get Arguments
            string eventName = inputArgs[0];
            string teamName = inputArgs[1];

            // Validate event
            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            // Validate team
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            // Check if current user if creator of event
            if (!CommandHelper.IsUserCreatorOfEvent(eventName, currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            // Add team to event
            AddTeamToEvent(eventName, teamName);

            return $"Team {teamName} added for {eventName}!";
        }

        private void AddTeamToEvent(string eventName, string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                Event evnt = context.Events
                    .OrderByDescending(e => e.StartDate)        // latest startDate event
                    .FirstOrDefault(e => e.Name == eventName);

                // Check if team is already added to event
                if (evnt.ParticipatingTeams.Any(t => t.Name == teamName))
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
                }

                evnt.ParticipatingTeams.Add(team);
                context.SaveChanges();
            }
        }
    }
}
