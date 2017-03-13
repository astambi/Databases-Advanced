namespace BookShopSystem.Migrations
{
    using Data;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    // internal sealed => public 
    public class Configuration : DbMigrationsConfiguration<BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;          // Step 4 
            ContextKey = "BookShopSystem.Data.BookShopContext";
            //AutomaticMigrationDataLossAllowed = true;   // Step 6 
        }

        protected override void Seed(BookShopContext context)
        {
            //  This method will be called after migrating to the latest version.
            Console.WriteLine("Seeding data");

            SeedAuthors(context);       // Step 5 
            SeedBooks(context);         // Step 5 
            SeedCategories(context);    // Step 5 
            base.Seed(context);
        }

        private void SeedCategories(BookShopContext context)
        {
            string[] categories = File.ReadAllLines("../../Import/categories.csv");
            int booksCount = context.Books.Local.Count;

            for (int i = 1; i < categories.Length; i++)
            {
                string[] data = categories[i]
                                .Split(',')
                                .Select(x => x.Replace("\"", string.Empty))
                                .ToArray();
                Category category = new Category() { Name = data[0] };

                int bookIndex = (i * 30) % booksCount;
                for (int j = 0; j < bookIndex; j++)
                {
                    Book book = context.Books.Local[j];
                    if (category.Books.FirstOrDefault(b => b == book) == null)
                    {
                        category.Books.Add(book);
                    }
                }
                context.Categories.AddOrUpdate(c => c.Name, category);
            }
            context.SaveChanges();
        }

        private void SeedBooks(BookShopContext context)
        {
            string[] books = File.ReadAllLines("../../Import/books.csv");
            int authorsCount = context.Authors.Local.Count;

            for (int i = 1; i < books.Length; i++)
            {
                string[] data = books[i]
                                .Split(',')
                                .Select(x => x.Replace("\"", string.Empty))
                                .ToArray();
                int authorIndex = i % authorsCount;
                Author author = context.Authors.Local[authorIndex];

                Book book = new Book()
                {
                    AuthorId = author.Id,
                    Edition = (EditionType)int.Parse(data[0]),
                    ReleaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture),
                    Copies = int.Parse(data[2]),
                    Price = decimal.Parse(data[3]),
                    AgeRestriction = (AgeRestriction)int.Parse(data[4]),
                    Title = data[5]
                };
                context.Books.AddOrUpdate(b => new { b.AuthorId, b.Title }, book);
            }
            context.SaveChanges();
        }

        private void SeedAuthors(BookShopContext context)
        {
            string[] authors = File.ReadAllLines("../../Import/authors.csv");

            for (int i = 1; i < authors.Length; i++)
            {
                string[] data = authors[i].Split(',');
                string firstName = data[0].Replace("\"", string.Empty);
                string lastName = data[1].Replace("\"", string.Empty);

                Author author = new Author()
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                context.Authors.AddOrUpdate(a => new { a.FirstName, a.LastName }, author);
            }
            context.SaveChanges();
        }
    }
}
