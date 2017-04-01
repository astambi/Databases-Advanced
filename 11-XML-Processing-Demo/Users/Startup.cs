namespace Users
{
    using System;
    using System.IO;

    class Startup
    {
        static void Main(string[] args)
        {
            InitializeDB();

            // Adding friend to first user & friend to user's friend
            AddFriends();

            // Creating trigger to create mutual friends
            CreateTrigger(); // run once only to create 
            AddFriendsWithTrigger();
        }

        private static void CreateTrigger()
        {
            Console.WriteLine("Creating trigger for mutual friends");
            using (var context = new UsersContext())
            {                
                string sqlQuery = File.ReadAllText("../../SQL/CreateTrigger_MutualFriends.sql");
                context.Database.ExecuteSqlCommand(sqlQuery);
            }
        }

        private static void AddFriendsWithTrigger()
        {
            using (var context = new UsersContext())
            {
                User pesho = new User() { Username = "Pesho" };
                User gosho = new User() { Username = "Gosho" };

                context.Users.AddRange(new[] { pesho, gosho });
                pesho.Friends.Add(gosho); // add friend to first user only
                context.SaveChanges();
            }
        }

        private static void AddFriends()
        {
            // Delete Trigger if existing

            using (var context = new UsersContext())
            {
                User pesho = new User() { Username = "Pesho" };
                User gosho = new User() { Username = "Gosho" };

                context.Users.AddRange(new[] { pesho, gosho });
                pesho.Friends.Add(gosho); // add friend to first user
                gosho.Friends.Add(pesho); // add friend to second user
                context.SaveChanges();
            }
        }

        private static void InitializeDB()
        {
            Console.WriteLine("Initializing database [Users]");
            using (var context = new UsersContext())
            {
                context.Database.Initialize(true);
            }
        }
    }
}
