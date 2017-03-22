namespace PhotoShare.Client.Core.Commands
{
    public class DeleteUser
    {
        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[1];
            //using (PhotoShareContext context = new PhotoShareContext())
            //{
            //    var user = context.Users.FirstOrDefault(u => u.Username == username);
            //    if (user == null)
            //    {
            //        throw new InvalidOperationException($"User with {username} was not found!");
            //    }

            //    // TODO: Delete User by username (only mark him as inactive)
            //    context.SaveChanges();

            //    return $"User {username} was deleted from the database!";
            //}
            return null;
        }
    }
}
