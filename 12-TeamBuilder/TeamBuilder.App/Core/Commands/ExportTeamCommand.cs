namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using Utilites;

    class ExportTeamCommand
    {
        public string JsonConvert { get; private set; }

        // ExportTeam <teamName>
        public string Execute(string[] inputArgs)
        {
            // Validate arguemnts count
            Check.CheckLength(1, inputArgs);

            string teamName = inputArgs[0];

            // Validate team
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            // Get team with members
            Team team = GetTeamWithMembersByName(teamName);

            // Export to JSON file
            ExportToFile(team);

            return $"Team {teamName} exported!";
        }

        private void ExportToFile(Team team)
        {
            // TODO Export to JSON using NewtonSoft 

            //string json = JsonConvert.SerializeObject(new
            //{
            //    Name = team.Name,
            //    Acronym = team.Acronym,
            //    Members = team.Members.Select(m => m.Username)
            //}, Formatting.Indented);

            //File.WriteAllText("../../Export/team.json", json);
        }

        private Team GetTeamWithMembersByName(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.FirstOrDefault(t => t.Name == teamName);
            };
        }
    }
}
