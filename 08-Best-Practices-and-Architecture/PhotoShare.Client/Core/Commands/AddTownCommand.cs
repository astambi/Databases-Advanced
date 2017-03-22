namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;

    public class AddTownCommand
    {
        private readonly TownService townService;       // add TownService

        public AddTownCommand(TownService townService)  // add TownService
        {
            this.townService = townService;
        }

        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            string townName = data[0];  // modified
            string country = data[1];   // modified

            if (this.townService.IsExistingTown(townName))
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            townService.AddTown(townName, country);

            return $"Town {townName} was added successfully!";
        }
    }
}
