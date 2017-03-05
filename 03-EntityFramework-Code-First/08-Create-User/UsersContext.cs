namespace _08_Create_User
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UsersContext : DbContext
    {
        // Your context has been configured to use a 'UsersContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_08_Create_User.UsersContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'UsersContext' 
        // connection string in the application configuration file.
        public UsersContext()
            : base("name=UsersContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}