namespace Users
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Friends = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public virtual ICollection<User> Friends { get; set; }
    }
}
