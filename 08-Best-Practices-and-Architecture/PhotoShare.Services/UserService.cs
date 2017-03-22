namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserService
    {
        public void AddUser(string username, string password, string email)
        {
            User user = new User            // Moved from RegisterUserCommand
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

                User userToUpdate = context.Users
                    .Include(u => u.BornTown)       // .Include("BornTown") navigation props without FK
                    .Include(u => u.CurrentTown)    // .Include("CurrentTown")
                    .FirstOrDefault(u => u.Id == updatedUser.Id);

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
                context.SaveChanges();

                //User userToDelete = context.Users.Find(user.Id);  // v.2
                //userToDelete.IsDeleted = true;
                //context.SaveChanges();
            }
        }


    }
}
