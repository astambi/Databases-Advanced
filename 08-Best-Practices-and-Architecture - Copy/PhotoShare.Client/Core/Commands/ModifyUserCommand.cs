namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Services;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        private readonly UserService userService;
        private readonly TownService townService;

        public ModifyUserCommand(UserService userService, TownService townService)
        {
            this.userService = userService;
            this.townService = townService;
        }

        // ModifyUser <username> <property> <new value> 
        public string Execute(string[] data)
        {
            string username = data[0];
            string property = data[1];
            string newValue = data[2];

            User user = this.userService.GetUserByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (property == "Password")
            {
                if (!newValue.Any(c => char.IsLower(c)) || !newValue.Any(c => char.IsDigit(c)))
                {
                    throw new ArgumentException($"Value {newValue} not valid.\nInvalid Password");
                }

                user.Password = newValue;
            }
            else if (property == "BornTown")
            {
                Town town = this.townService.GetTownByName(newValue);
                if (town == null)
                {
                    throw new ArgumentException($"Value {newValue} not valid.\nTown {newValue} not found!");
                }

                user.BornTown = town; // TODO not saved ????
            }
            else if (property == "CurrentTown")
            {
                Town town = this.townService.GetTownByName(newValue);
                if (town == null)
                {
                    throw new ArgumentException($"Value {newValue} not valid.\nTown {newValue} not found!");
                }

                user.CurrentTown = town; // TODO not saved ????
            }
            else
            {
                throw new ArgumentException($"Property {property} not supported!");
            }

            userService.UpdateUser(user);

            return $"User {username} {property} is {newValue}.";
        }
    }
}
