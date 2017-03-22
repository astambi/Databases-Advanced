namespace PhotoShare.Services
{
    using Models;

    public static class AuthenticationService
    {
        private static User currentUser;

        public static bool IsAuthenticated()
        {
            return currentUser != null;
        }

        public static void Login(User user)
        {
            currentUser = user;
        }

        public static void Logout()
        {
            currentUser = null;
        }

        public static User GetCurrentUser()
        {
            return currentUser;
        }
    }
}
