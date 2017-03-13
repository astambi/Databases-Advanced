namespace _11_Banking_System.Core
{
    using Models;
    using System;

    public class AuthenticationManager
    {
        private static User currentUser;

        public static bool IsAuthenticated()
        {
            return currentUser != null;
        }

        public static void Logout()
        {
            if (!IsAuthenticated())
            {
                throw new InvalidOperationException("Cannot log out. No user was logged in.");
            }
            currentUser = null;
        }

        public static void Login(User user)
        {
            if (IsAuthenticated())
            {
                throw new InvalidOperationException("You should logout first.");
            }
            if (user == null)
            {
                throw new InvalidOperationException("Incorrect username.");
            }
            currentUser = user;
        }

        public static User GetCurrentUser()
        {
            return currentUser;
        }
    }
}
