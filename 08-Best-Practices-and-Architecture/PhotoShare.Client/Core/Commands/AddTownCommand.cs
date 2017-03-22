namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;

    public class AddTownCommand
    {
        private readonly TownService townService; 

        public AddTownCommand(TownService townService)
        {
            this.townService = townService;
        }

        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];

            // 2. Extend Photo Share System
            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials! You should log in first.");
            }

            if (this.townService.IsExistingTown(townName))
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            townService.AddTown(townName, country);

            return $"Town {townName} was added successfully!";
        }
    }
}
