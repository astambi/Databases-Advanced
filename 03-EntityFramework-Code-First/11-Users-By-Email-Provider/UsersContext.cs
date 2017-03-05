namespace _11_Users_By_Email_Provider
{
    using _12_Remove_Inactive_Users;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UsersContext : DbContext
    {
        // Your context has been configured to use a 'UsersContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_11_Users_By_Email_Provider.UsersContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'UsersContext' 
        // connection string in the application configuration file.
        public UsersContext()
            : base("name=UsersContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }
    
}