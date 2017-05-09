namespace Stations.Data
{
    using System;

    public static class Init
    {
        public static void InitializeDatabase()
        {
            using (var context = new StationsContext())
            {
                Console.WriteLine("Initializing database");
                context.Database.Initialize(true);
            }
        }
    }
}
