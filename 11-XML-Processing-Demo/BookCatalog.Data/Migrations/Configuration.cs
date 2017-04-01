namespace BookCatalog.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookCatalogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BookCatalog.Data.BookCatalogContext";
        }

        protected override void Seed(BookCatalogContext context)
        {
            //  This method will be called after migrating to the latest version.           

        }
    }
}
