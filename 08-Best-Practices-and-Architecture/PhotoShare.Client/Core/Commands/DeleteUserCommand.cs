using PhotoShare.Models;
using PhotoShare.Services;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class DeleteUserCommand
    {
        private readonly UserService userService;
        public DeleteUserCommand(UserService userService)
        {
            this.userService = userService;
        }

        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[0];

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
                throw new InvalidOperationException("Invalid credentials! You can delete your own profile only.");
            }

            User user = this.userService.GetUserByUsername(username);

            if (user.IsDeleted == true)
            {
                throw new InvalidOperationException($"User {username} is already deleted!");
            }

            //userService.DeleteUser(user);
            userService.DeleteUser(username);

            return $"User {username} was deleted successfully!";
        }
    }
}
