namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using Utilites;
    using System.Linq;

    class DeleteUserCommand
    {
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(0, inputArgs);

            // Check if user is already authenticated, if not => Login first
            AuthenticationManager.Authorize();

            // Delete user
            User currentUser = AuthenticationManager.GetCurrentUser();
            this.DeleteUser(currentUser);

            // Logout
            AuthenticationManager.Logout();

            return $"User {currentUser.Username} was deleted successfully!";
        }

        private void DeleteUser(User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Users
                    .FirstOrDefault(u => u.Username == user.Username) // get user
                    .IsDeleted = true;
                context.SaveChanges();

                //context.Users.Attach(user); // OR attach user
                //user.IsDeleted = true;
                //context.SaveChanges();
            }
        }
    }
}
