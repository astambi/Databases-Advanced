namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Services;
    using System;

    public class LoginCommand
    {
        private readonly UserService userService;

        public LoginCommand(UserService userService)
        {
            this.userService = userService;
        }

        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];

            if (AuthenticationService.IsAuthenticated())
            {
                throw new ArgumentException("You should log out first!");
            }

            if (userService.GetUserByUsername(username).IsDeleted == true)
            {
                throw new ArgumentException($"User {username} was deleted!");
            }

            if (!this.userService.HasValidUserCredentials(username, password))
            {
                throw new ArgumentException("Invalid username or password!");
            }

            User user = this.userService.GetUserByUsername(username);

            AuthenticationService.Login(user);

            return $"User {username} successfully logged in!";
        }
    }
}
