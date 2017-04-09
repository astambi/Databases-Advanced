namespace PlanetHunters.Data
{
    public static class Init
    {
        public static void InitializeDatabase()
        {
            using (var context = new PlanetHuntersContext())
            {
                System.Console.WriteLine("Initializing database PlanetHunters");
                context.Database.Initialize(true);
            }
        }
    }
}
