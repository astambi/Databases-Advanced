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

            if (!this.userService.IsExistingUser(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            User user = this.userService.GetUserByUsername(username);

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
                if (!this.townService.IsExistingTown(newValue))
                {
                    throw new ArgumentException($"Value {newValue} not valid.\nTown {newValue} not found!");
                }

                user.BornTown = this.townService.GetTownByName(newValue);
            }
            else if (property == "CurrentTown")
            {
                if (!this.townService.IsExistingTown(newValue))
                {
                    throw new ArgumentException($"Value {newValue} not valid.\nTown {newValue} not found!");
                }

                user.CurrentTown = this.townService.GetTownByName(newValue);
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
