namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using Utilites;

    class CreateTeamCommand
    {
        // CreateTeam <name> <acronym> <description>
        // Description is optional
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            int inputArgsCount = inputArgs.Length;
            if (inputArgsCount != 2 && inputArgsCount != 3)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
                //throw new ArgumentOutOfRangeException(nameof(inputArgs));
            }

            // Authorize user
            AuthenticationManager.Authorize();

            // Get Arguments
            string teamName = inputArgs[0];
            string acronym = inputArgs[1];
            string description = inputArgsCount == 3 ? inputArgs[2] : null;

            // Validate teamname length (optional)
            if (teamName.Length > Constants.MaxTeamNameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InvalidTeamName, teamName));
            }

            // Check if teamName already exists
            if (CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamExists, teamName));
            }

            // Validate acronym
            if (acronym.Length != Constants.ExactAcronymLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InvalidAcronym, acronym));
            }

            // Validate description (optional)
            if (description != null && description.Length > Constants.MaxTeamDescriptionLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InvalidTeamDescription, description));
            }

            // Create team
            CreateTeam(teamName, acronym, description);

            return $"Team {teamName} successfully created!";
        }

        private void CreateTeam(string teamName, string acronym, string description)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = new Team()
                {
                    Name = teamName,
                    Acrinym = acronym,
                    Description = description,
                    CreatorId = AuthenticationManager.GetCurrentUser().Id // current user
                };

                context.Teams.Add(team);
                context.SaveChanges();
            }
        }
    }
}
