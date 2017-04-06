namespace TeamBuilder.App.Core.Commands
{
    using Models;
    using Utilites;

    class LogoutCommand
    {
        // Logout
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(0, inputArgs);

            // Check if user is already authenticated, if not => Login first
            AuthenticationManager.Authorize();

            // Logout
            User currentUser = AuthenticationManager.GetCurrentUser();
            AuthenticationManager.Logout();

            return $"User {currentUser.Username} successfully logged out!";
        }
    }
}
