namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class UserService
    {
        public void AddUser(string username, string password, string email)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    IsDeleted = false,
                    RegisteredOn = DateTime.Now,
                    LastTimeLoggedIn = DateTime.Now
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public bool IsExistingUser(string username)
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

        public void UpdateUser(string username, string property, string newValue)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users
                    .Include(u => u.BornTown)       // or .Include("BornTown") 
                    .Include(u => u.CurrentTown)    // or .Include("CurrentTown")
                    .SingleOrDefault(u => u.Username == username);

                switch (property)
                {
                    case "Password":
                        user.Password = newValue;
                        break;
                    case "BornTown":
                        user.BornTown = context.Towns.SingleOrDefault(t => t.Name == newValue);
                        break;
                    case "CurrentTown":
                        user.CurrentTown = context.Towns.SingleOrDefault(t => t.Name == newValue);
                        break;
                }

                context.SaveChanges();
            }
        }

        public void UpdateUser(User updatedUser)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                /* Alternative approach:
                 * Modify User Model => Add BornTownId/ CurrentTownId
                 * context.Users.Attach(user);
                 * context.Entry(user).State = EntityState.Modified; 
                 * context.SaveChanges();
                 */

                User userToUpdate = context.Users   // Include navigation props without FK
                    .Include(u => u.BornTown)       // or .Include("BornTown") 
                    .Include(u => u.CurrentTown)    // or .Include("CurrentTown")
                    .SingleOrDefault(u => u.Id == updatedUser.Id);

                // NB Disable lazy loading in Context! or modify User Model (add BornTownId/ CurrentTownId)
                if (userToUpdate != null)
                {
                    if (updatedUser.Password != userToUpdate.Password)
                    {
                        userToUpdate.Password = updatedUser.Password;
                    }

                    if (updatedUser.BornTown != null &&
                        (userToUpdate.BornTown == null || updatedUser.BornTown.Id != userToUpdate.BornTown.Id))
                    {
                        userToUpdate.BornTown = context.Towns.Find(updatedUser.BornTown.Id);
                        //userToUpdate.BornTown = updatedUser.BornTown; // NB Incorrect! Would insert a NEW town
                    }

                    if (updatedUser.CurrentTown != null &&
                       (userToUpdate.CurrentTown == null || updatedUser.CurrentTown.Id != userToUpdate.CurrentTown.Id))
                    {
                        userToUpdate.CurrentTown = context.Towns.Find(updatedUser.CurrentTown.Id);
                        //userToUpdate.CurrentTown = updatedUser.CurrentTown; // NB Incorrect! Would insert a NEW town
                    }

                    context.SaveChanges();
                }
            }
        }

        public void DeleteUser(User user)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                user.IsDeleted = true;                              // v.1
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Modified;

                //User userToDelete = context.Users.Find(user.Id);  // v.2
                //userToDelete.IsDeleted = true;

                context.SaveChanges();
            }
        }

        public void DeleteUser(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                context.Users
                    .SingleOrDefault(u => u.Username == username)
                    .IsDeleted = true;

                context.SaveChanges();
            }
        }

        public bool IsFriendToUser(string userUsername, string friendUsername)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users
                    .Include(u => u.Friends)
                    .SingleOrDefault(u => u.Username == userUsername);

                return user.Friends.Any(f => f.Username == friendUsername);
            }
        }

        public void MakeFriends(string username1, string username2)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user1 = context.Users.SingleOrDefault(u => u.Username == username1);
                User user2 = context.Users.SingleOrDefault(u => u.Username == username2);

                user1.Friends.Add(user2);   // user2 friend to user1
                user2.Friends.Add(user1);   // user1 friend to user2

                context.SaveChanges();
            }
        }

        public List<string> GetUserFriends(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users
                    .Include(u => u.Friends)
                    .SingleOrDefault(u => u.Username == username)
                    .Friends
                    .Select(f => f.Username)    // username only
                    .OrderBy(u => u)            // ASC
                    .ToList();
            }
        }

        public bool HasValidUserCredentials(string username, string password)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.Any(u => u.Username == username && u.Password == password);
            }
        }

        public bool HasProfileRights(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return AuthenticationService.GetCurrentUser().Username == username;
            }
        }
    }
}
