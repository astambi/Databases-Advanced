namespace Demo.Data
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class QueryContext : DbContext
    {
        public QueryContext()
            : base("name=QueryContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QueryContext, Configuration>());
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}