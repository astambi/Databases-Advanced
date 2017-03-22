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

            if (!this.userService.IsExistingUser(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            User user = this.userService.GetUserByUsername(username);

            if (user.IsDeleted == true)
            {
                throw new InvalidOperationException($"User {username} is already deleted!");
            }

            userService.DeleteUser(user);

            return $"User {username} was deleted successfully!";
        }
    }
}
