namespace DemoFootballManager
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FootballManagerContext : DbContext
    {
        /* Your context has been configured to use a 'FootballManagerContext' connection string from your application's configuration file (App.config or Web.config). 
         * By default, this connection string targets the 'DemoFootballManager.FootballManagerContext' database on your LocalDb instance.
         * If you wish to target a different database and/or database provider, modify the 'FootballManagerContext' connection string in the application configuration file.         
         * Add a DbSet for each entity type that you want to include in your model.For more information on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109. 
        */
        public FootballManagerContext()
            : base("name=FootballManagerContext")
        {
        }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
    }

    /* public class MyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    } */
}