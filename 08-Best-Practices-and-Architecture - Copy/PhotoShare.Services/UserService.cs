namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserService
    {
        public void Add(string username, string password, string email)
        {
            // Moved from RegisterUserCommand
            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public bool IsTaken(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.Any(u => u.Username == username);
            }
        }
        
        public User GetUserByUsername(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.SingleOrDefault(u => u.Username == username);
            }
        }

        public void UpdateUser(User user)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
