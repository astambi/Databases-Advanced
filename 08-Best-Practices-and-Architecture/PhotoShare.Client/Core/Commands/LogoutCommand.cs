namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Services;
    using System;

    public class LogoutCommand
    {
        public string Execute()
        {

            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            User user = AuthenticationService.GetCurrentUser();

            AuthenticationService.Logout();

            return $"User {user.Username} successfully logged out!";
        }
    }
}
