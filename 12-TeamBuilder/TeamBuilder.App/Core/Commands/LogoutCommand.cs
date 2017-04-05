namespace TeamBuilder.App.Core.Commands
{
    using Models;
    using System;
    using Utilites;

    class LogoutCommand
    {
        // Logout
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(0, inputArgs);

            // Check if user is already authenticated
            AuthenticationManager.Authorize();

            User user = AuthenticationManager.GetCurrentUser();
            AuthenticationManager.Logout();

            return $"User {user.Username} successfully logged out!";
        }
    }
}
