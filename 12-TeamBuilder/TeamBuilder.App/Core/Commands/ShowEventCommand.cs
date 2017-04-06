namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilites;

    class ShowEventCommand
    {
        // ShowEvent <eventName>
        public string Execute(string[] inputArgs)
        {
            // Validate input arguments count
            Check.CheckLength(1, inputArgs);

            string eventName = inputArgs[0];

            // Validate event
            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            return GetEventDetails(eventName);
        }

        private string GetEventDetails(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Event evnt = context.Events
                    .OrderByDescending(e => e.StartDate)
                    .FirstOrDefault(e => e.Name == eventName);
                List<string> teams = evnt.ParticipatingTeams.Select(t => t.Name).ToList();

                string result = $"{evnt.Name} {evnt.StartDate} {evnt.EndDate}\n{evnt.Description}\nTeams:";
                if (teams.Count() > 0)
                    result += $"\n-{string.Join("\n-", teams)}";

                return result;
            }
        }
    }
}
