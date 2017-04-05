namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilites;

    class LoginCommand
    {
        // Login <username> <password>
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(2, inputArgs);

            string username = inputArgs[0];
            string password = inputArgs[1];

            // Check if user is already authenticated
            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            // Validate user
            User user = this.GetUserByCredentials(username, password);
            if (user == null)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserOrPasswordIsInvalid));
            }

            AuthenticationManager.Login(user);

            return $"User {username} successfully logged in!";
        }

        private User GetUserByCredentials(string username, string password)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == username &&
                                                         u.Password == password &&
                                                         u.IsDeleted == false);
            }
        }
    }
}
