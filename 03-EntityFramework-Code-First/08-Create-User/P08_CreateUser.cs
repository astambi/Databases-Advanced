using _07_Gringotts_Database;
using _08_Create_User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_Create_User
{
    class P08_CreateUser
    {
        static void Main(string[] args)
        {
            UsersContext context = new UsersContext();
            context.Database.Initialize(true);
            SeedUsers(context);
        }

        private static void SeedUsers(UsersContext context)
        {
            context.Users.Add(new User("testUser", "stupidPASS123!@#$%", "test-user@gmail.com")
            {
                Age = 22,
                LastTimeLoggedIn = new DateTime(2016, 12, 31),
                IsDeleted = true
            });
            context.Users.Add(new User("Kevin", "myPASS123@@@", "123kevin.smi-th_987@gmail.com")
            {
                Age = 35,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now,
                IsDeleted = false
            });
            context.Users.Add(new User("Maria", "marryMARRY@#$123", "marry_Mary098.4@hotmail.com")
            {
                Age = 15
            });
            context.Users.Add(new User("Nakov", "Nakov45678SVETLIN@#$", "nakov@softuni.bg")
            {
                Age = 36,
                RegisteredOn = new DateTime(2010, 1, 20),
                LastTimeLoggedIn = DateTime.Now,
                IsDeleted = false,
                ProfilePicture = 1000000
            });

            // uncomment to test Regex email validation 
            //context.Users.Add(new User("Test", "abcABC123@#$", "test.@hotmail.com"));
            //context.Users.Add(new User("Test", "abcABC123@#$", "111test@hotmail.com"));
            //context.Users.Add(new User("Test", "abcABC123@#$", "...test@hotmail.com"));
            //context.Users.Add(new User("Test", "abcABC123@#$", "test-123@hotmail.com123"));
            //context.Users.Add(new User("Test", "abcABC123@#$", "test-123@hotmail123.com"));
            //context.Users.Add(new User("Test", "abcABC123@#$", "test-123@hotmailcom"));
            //context.Users.Add(new User("Test", "abcABC123@#$", "test-123@hotmail....com"));

            // uncomment to test Regex password validation 
            //context.Users.Add(new User("Test", "A..1@", "test-123@hotmail.com"));
            //context.Users.Add(new User("Test", "A...1@", "test-123@hotmail.com"));
            //context.Users.Add(new User("Test", ".aaa1@", "test-123@hotmail.com"));
            //context.Users.Add(new User("Test", "Aaaa.@", "test-123@hotmail.com"));
            //context.Users.Add(new User("Test", "Aaaa1.", "test-123@hotmail.com"));

            context.SaveChanges();
        }
    }
}