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

            // 2. Extend Photo Share System
            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials! You should log in first.");
            }

            if (!this.userService.IsExistingUser(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            // 2. Extend Photo Share System
            if (!userService.HasProfileRights(username))
            {
                throw new InvalidOperationException("Invalid credentials! You can modify your own profile only.");
            }

            //User user = this.userService.GetUserByUsername(username); // v.2

            if (property == "Password")
            {
                if (!newValue.Any(c => char.IsLower(c)) || 
                    !newValue.Any(c => char.IsDigit(c)))
                {
                    throw new ArgumentException($"Value {newValue} not valid.\nInvalid Password");
                }
                //user.Password = newValue; // v.2
            }
            else if (property == "BornTown")
            {
                if (!this.townService.IsExistingTown(newValue))
                {
                    throw new ArgumentException($"Value {newValue} not valid.\nTown {newValue} not found!");
                }
                //user.BornTown = this.townService.GetTownByName(newValue); // v.2
            }
            else if (property == "CurrentTown")
            {
                if (!this.townService.IsExistingTown(newValue))
                {
                    throw new ArgumentException($"Value {newValue} not valid.\nTown {newValue} not found!");
                }
                //user.CurrentTown = this.townService.GetTownByName(newValue); // v.2
            }
            else
            {
                throw new ArgumentException($"Property {property} not supported!");
            }

            //userService.UpdateUser(user); // v.2
            userService.UpdateUser(username, property, newValue); // v.1

            return $"User {username} {property} is {newValue}.";
        }
    }
}
