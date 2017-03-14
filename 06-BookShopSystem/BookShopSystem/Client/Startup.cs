using BookShopSystem.Data;
using BookShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BookShopSystem
{
    class Startup
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solutions to EF Code First - BookShop with Automatic Migrations\n");
            BookShopContext context = new BookShopContext();

            // Step 4 => moved to BookShopContext
            //var migrationStrategy = new MigrateDatabaseToLatestVersion<BookShopContext, Configuration>();
            //Database.SetInitializer(migrationStrategy);

            Console.WriteLine("Initializing database [BookShopSystem.CodeFirst]");
            context.Database.Initialize(true);

            SetRelatedBooks(context);
        }

        private static void SetRelatedBooks(BookShopContext context)
        {
            Console.WriteLine("\n**************************************************************");
            Console.WriteLine("Solution to Step 6.Related Books - Make first 3 books related\n");

            // Set Related Books
            List<Book> books = context.Books.Take(3).ToList();
            books[0].RelatedBooks.Add(books[1]);
            books[1].RelatedBooks.Add(books[0]);
            books[0].RelatedBooks.Add(books[2]);
            books[2].RelatedBooks.Add(books[0]);
            context.SaveChanges();

            // Query Related Books
            books = context.Books.Take(3).ToList();
            foreach (Book book in books)
            {
                Console.WriteLine($"--{book.Title}");
                foreach (Book relatedBook in book.RelatedBooks)
                {
                    Console.WriteLine(relatedBook.Title);
                }
            }
        }
    }
}
