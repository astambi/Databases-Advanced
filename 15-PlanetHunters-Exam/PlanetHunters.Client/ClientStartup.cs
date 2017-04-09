namespace PlanetHunters.Client
{
    using Data;

    class ClientStartup
    {
        static void Main(string[] args)
        {
            Init.InitializeDatabase();
        }
    }
}
