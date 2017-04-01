namespace BookCatalog.Data
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class BookCatalogContext : DbContext
    {
        public BookCatalogContext()
            : base("name=BookCatalogContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookCatalogContext, Configuration>());
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Genre>()
            //    .Property(t => t.Name)
            //    .HasColumnAnnotation("Index",
            //        new IndexAnnotation(new IndexAttribute("IX_GenreName") { IsUnique = true }));

            base.OnModelCreating(modelBuilder);
        }
    }
}