namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;

    public class RegisterUserCommand
    {
        private readonly UserService userService;

        public RegisterUserCommand(UserService userService) 
        {
            this.userService = userService;
        }

        //RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            // 2. Extend Photo Share System
            if (AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should log out first!");
            }

            if (this.userService.IsExistingUser(username))
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            userService.AddUser(username, password, email);

            return $"User {username} was registered successfully!";
        }
    }
}
