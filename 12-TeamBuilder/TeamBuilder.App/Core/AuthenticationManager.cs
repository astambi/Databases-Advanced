namespace TeamBuilder.App.Core
{
    using Models;
    using System;
    using Utilites;

    class AuthenticationManager
    {
        private static User currentUser;

        public static void Login(User user)
        {
            currentUser = user;
        }

        public static void Logout()
        {
            Authorize();
            currentUser = null;
        }

        public static void Authorize()
        {
            if (currentUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public static bool IsAuthenticated()
        {
            return currentUser != null;
        }

        public static User GetCurrentUser()
        {
            Authorize();
            return currentUser;
        }
    }
}
