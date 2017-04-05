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

            User currentUser = AuthenticationManager.GetCurrentUser();
            this.DeleteUser(currentUser);
            AuthenticationManager.Logout();

            return $"User {currentUser.Username} was deleted successfully!";
        }

        private void DeleteUser(User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                // Get user and set status to deleted
                context.Users
                    .FirstOrDefault(u => u.Username == user.Username)
                    .IsDeleted = true;

                // OR Attach user and set status to deleted //

                //context.Users.Attach(user); // attach user
                //user.IsDeleted = true;

                context.SaveChanges();
            }
        }
    }
}
