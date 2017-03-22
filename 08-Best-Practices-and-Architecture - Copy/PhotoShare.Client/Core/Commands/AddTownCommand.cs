namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;

    public class AddTownCommand
    {
        private readonly TownService townService; // added service

        public AddTownCommand(TownService townService) // added service
        {
            this.townService = townService;
        }

        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            string townName = data[0];  // modified
            string country = data[1];   // modified

            if (this.townService.IsExisting(townName))
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            townService.Add(townName, country);

            return $"Town {townName} was added successfully!"; // modified
        }
    }
}
