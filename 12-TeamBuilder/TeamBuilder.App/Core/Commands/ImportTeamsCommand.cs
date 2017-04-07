namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;
    using Utilites;

    class ImportTeamsCommand
    {
        // ImportTeams <filePathToXmlFile>
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(1, inputArgs);

            string filePath = inputArgs[0];

            // Validate file path
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format(Constants.ErrorMessages.FileNotFound, filePath));
            }

            // Read teams from XML
            List<Team> teams;
            try
            {
                teams = GetTeamsFromXml(filePath);
            }
            catch (Exception)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidXmlFormat);
            }

            // Add teams to database
            AddTeams(teams);

            return $"You have successfully imported {teams.Count} teams!";
        }

        private void AddTeams(List<Team> teams)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Teams.AddRange(teams);
                context.SaveChanges();
            }
        }

        private List<Team> GetTeamsFromXml(string filePath)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                XDocument xmlDoc = XDocument.Load(filePath);
                var teamsData = xmlDoc.Root.Elements();

                List<Team> teams = new List<Team>();
                foreach (XElement t in teamsData)
                {
                    Team team = new Team()
                    {
                        Name = t.Element("name").Value,
                        Acronym = t.Element("acronym").Value,
                        Description = t.Element("description").Value,
                        CreatorId = int.Parse(t.Element("creator-id").Value),
                    };
                    teams.Add(team);
                }

                return teams;
            }
        }
    }
}
