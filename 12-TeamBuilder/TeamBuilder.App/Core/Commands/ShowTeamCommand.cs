namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilites;

    class ShowTeamCommand
    {
        // ShowTeam <teamName>
        public string Execute(string[] inputArgs)
        {
            // Validate input arguments count
            Check.CheckLength(1, inputArgs);

            string teamName = inputArgs[0];

            // Validate team
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            return GetTeamDetails(teamName);
        }

        private string GetTeamDetails(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                List<string> members = team.Members.Select(m => m.Username).ToList();

                string result = $"{team.Name} {team.Acronym}\nMembers:";
                if (members.Count() > 0)
                    result += $"\n--{string.Join("\n--", members)}";

                return result;
            }
        }
    }
}
