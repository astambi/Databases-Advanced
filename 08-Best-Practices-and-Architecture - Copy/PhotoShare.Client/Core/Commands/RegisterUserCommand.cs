namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;

    public class RegisterUserCommand
    {
        private readonly UserService userService; // added UserService

        public RegisterUserCommand(UserService userService) // added UserService
        {
            this.userService = userService;
        }

        //RegisterUser<username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            if (password != repeatPassword) // modified
            {
                throw new ArgumentException("Passwords do not match!");
            }

            // Adding user to context moved to UserService
            if (this.userService.IsTaken(username))
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            userService.Add(username, password, email);

            return "User " + username + " was registered successfully!"; // modified
        }
    }
}
