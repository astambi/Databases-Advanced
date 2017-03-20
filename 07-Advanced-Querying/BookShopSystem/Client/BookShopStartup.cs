using BookShopSystem.Data;
using BookShopSystem.Models;
using EntityFramework.Extensions;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;

namespace BookShopSystem
{
    class BookShopStartup
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solutions to Advanced Querying - BookShop System\n");

            BookShopContext context = new BookShopContext();
            Console.WriteLine("Initializing database [BookShopSystem.CodeFirst]");
            context.Database.Initialize(true);

            //Solutions
            BookTitlesByAgeRestrictionV1(context);
            BookTitlesByAgeRestrictionV2(context);
            GoldenBooks(context);
            BooksByPrice(context);
            NotReleasedBooks(context);
            BookTitlesByCategory(context);
            BooksReleasedBeforeDate(context);
            AuthorsSearch(context);
            BookSearch(context);
            BookTitleSearch(context);
            CountBooks(context);
            TotalBookCopies(context);
            FindProfit(context);
            MostRecentBooks(context);
            IncreaseBookCopies(context);
            RemoveBooks(context);
            StoredProcedure(context);
        }

        private static void StoredProcedure(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 15. Stored Procedure\n"+
                "To have a correct number of books run this solution after Problem 13. Increase Book Copies & 14. Remove Books");

            while (true)
            {
                Console.Write("1. Create a stored procedure (once only)\n" +
                    "2. Start querying (if the stored procedure is already created)\n" +
                    "Enter your choice or [end]: ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1": CreateStoredProcedure(context); break;
                    case "2": ExecuteStoredProcedure(context); break;
                    case "end": break;
                    default: Console.WriteLine("Invalid option."); break;
                }
                if (option == "end" || option == "2") break;
            }
        }

        private static void ExecuteStoredProcedure(BookShopContext context)
        {
            while (true)
            {
                Console.Write("Enter Author [FirstName LastName] or [end]\ne.g. [Amanda Rice/ Beverly Ford/ Wanda Morales]: ");
                string input = Console.ReadLine();
                if (input == "end") break;

                string[] names = Regex.Split(input, @"\s+");
                if (names.Length == 2)
                {
                    string firstName = names[0];
                    string lastName = names[1];
                    SqlParameter firstNameParam = new SqlParameter("@firstName", firstName);
                    SqlParameter lastNameParam = new SqlParameter("@lastName", lastName);
                    int booksCount = context.Database
                        .SqlQuery<int>("EXEC dbo.usp_CountBooksByAuthor @firstName, @lastName", firstNameParam, lastNameParam)
                        .First();
                    Console.WriteLine($"{firstName} {lastName} has written {booksCount} books");
                }
                else Console.WriteLine("Invalid number of arguments");
            }
        }

        private static void CreateStoredProcedure(BookShopContext context)
        {
            string sqlQueryCreateProc = File.ReadAllText(@"../../SqlQueries/CreatePROC_CountBooksByAuthor.sql");
            Console.WriteLine("Creating Stored Procedure [dbo.usp_CountBooksByAuthor]");
            context.Database.ExecuteSqlCommand(sqlQueryCreateProc);
        }

        private static void RemoveBooks(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 14. Remove Books with copies < 4200");

            var booksToRemove = context.Books.Where(b => b.Copies < 4200);
            Console.WriteLine($"{booksToRemove.Count()} books were deleted");

            booksToRemove.Delete();
            context.SaveChanges();

            // Just a check
            //int remainingBooksCount = context.Books.Where(b => b.Copies < 4200).Count();
            //Console.WriteLine($"Check: {remainingBooksCount} books with copies < 4200 remaining after Bulk Delete"); // 0 => all books are deleted
            Pause();
        }

        private static void IncreaseBookCopies(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 13. Increase Book Copies");

            var booksToUpdate = context.Books.Where(b => b.ReleaseDate > new DateTime(2013, 6, 6));
            int bookCopiesBeforeUpdate = booksToUpdate.Sum(b => b.Copies);

            booksToUpdate.Update(b => new Book() { Copies = b.Copies + 44 });
            context.SaveChanges();

            int bookCopiesAfterUpdate = booksToUpdate.Sum(b => b.Copies);

            Console.WriteLine($"{booksToUpdate.Count()} books are released after 6 Jun 2013 so total of {bookCopiesAfterUpdate - bookCopiesBeforeUpdate} book copies were added"); // actual number of added copies

            //Console.WriteLine($"{booksToUpdate.Count()} books are released after 6 Jun 2013 so total of {booksToUpdate.Count() * 44} book copies were added"); // calculated number of added copies after Bulk Update

            Pause();
        }

        private static void MostRecentBooks(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 12. Most Recent Books");
            Console.WriteLine("\nCategory, total number of books (> 35) & top 3 most recent books:");

            var categories = context.Categories
                .Select(c => new { c.Name, TotalBooks = c.Books.Count() })
                .Where(c => c.TotalBooks > 35)
                .OrderByDescending(c => c.TotalBooks);

            foreach (var category in categories)
            {
                Console.WriteLine($"--{category.Name}: {category.TotalBooks} books");
                context.Books
                    .Where(b => b.Categories.Any(c => c.Name == category.Name))
                    .OrderByDescending(b => b.ReleaseDate)
                    .ThenBy(b => b.Title)
                    .Take(3)
                    .Select(b => new { b.Title, ReleaseYear = b.ReleaseDate.Value.Year })
                    .ToList()
                    .ForEach(b => Console.WriteLine($"{b.Title} ({b.ReleaseYear})"));
            }
            Pause();
        }

        private static void FindProfit(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 11. Find Profit");
            Console.WriteLine("\nTotal profit by book category, by profit DESC & category ASC:");

            context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.Books.Sum(b => b.Copies * b.Price)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .ToList()
                .ForEach(c => Console.WriteLine($"{c.CategoryName} - ${c.TotalProfit:f2}"));
            Pause();
        }

        private static void TotalBookCopies(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 10. Total Book Copies");
            Console.WriteLine("\nAuthors & total number of book copies, by copies DESC:");

            context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    BookCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.BookCopies)
                .ToList()
                .ForEach(a => Console.WriteLine($"{a.FirstName} {a.LastName} - {a.BookCopies}"));
            Pause();
        }

        private static void CountBooks(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 9. Count Books");

            while (true)
            {
                Console.Write("Enter title length: ");
                int length;
                if (int.TryParse(Console.ReadLine(), out length))
                {
                    int booksCount = context.Books
                        .Where(b => b.Title.Length > length)
                        .Count();
                    Console.WriteLine($"\nThere are {booksCount} books with longer title than {length} symbols");
                    break;
                }
                else Console.Write("Invalid input. ");
            }
            Pause();
        }

        private static void BookTitleSearch(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 8. Book Title Search (author's last name starting with [string])");
            Console.Write("Enter search criteria: ");
            string searchValue = Console.ReadLine();

            Console.WriteLine($"\nBooks with authors whose last names starts with [{searchValue}], by Id ASC:");
            context.Books
                .Where(b => b.Author.LastName.StartsWith(searchValue))
                .OrderBy(b => b.Id)
                .ToList()
                .ForEach(b => Console.WriteLine($"{b.Title} ({b.Author.FirstName} {b.Author.LastName})"));
            Pause();
        }

        private static void BookSearch(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 7. Book Search (title containing [string])");
            Console.Write("Enter search criteria: ");
            string searchValue = Console.ReadLine();

            Console.WriteLine($"\nBooks with titles containing [{searchValue}]:");
            context.Books
                .Where(b => b.Title.Contains(searchValue))
                .ToList()
                .ForEach(b => Console.WriteLine(b.Title));
            Pause();
        }

        private static void AuthorsSearch(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 6. Author Search (first name ending with [string])");
            Console.Write("Enter search criteria: ");
            string searchValue = Console.ReadLine();

            Console.WriteLine($"\nAuthors with first names ending with [{searchValue}]:");
            context.Authors
                .Where(a => a.FirstName.EndsWith(searchValue))
                .ToList()
                .ForEach(a => Console.WriteLine(string.Concat(a.FirstName, ' ', a.LastName)));
            Pause();
        }

        private static void BooksReleasedBeforeDate(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 5. Books Released Before Date");

            while (true)
            {
                Console.Write("Enter date in format [dd-MM-yyyy]: ");
                DateTime date;

                if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    Console.WriteLine($"\nBooks released before {date:dd-MM-yyyy}:");
                    context.Books
                        .Where(b => b.ReleaseDate != null && b.ReleaseDate < date)
                        .ToList()
                        .ForEach(b => Console.WriteLine($"{b.Title} - {b.Edition} - {b.Price:f2}"));
                    break;
                }
                else Console.Write("Invalid input. ");
            }
            Pause();
        }

        private static void BookTitlesByCategory(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 4. Book Titles by Category");
            Console.Write("Enter categories (separated by one or more spaces): ");
            string[] categoryNames = Regex.Split(Console.ReadLine().Trim(), @"\s+")
                                    .Select(c => c.ToLower())
                                    .ToArray();
            Console.WriteLine($"\nBooks with categories [{string.Join(",", categoryNames)}] by Id ASC: ");
            context.Books
                .Where(b => b.Categories.Any(c => categoryNames.Contains(c.Name.ToLower())))
                .OrderBy(b => b.Id)
                .ToList()
                .ForEach(b => Console.WriteLine(b.Title));

            Pause();
        }

        private static void NotReleasedBooks(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 3. Not Released Books");

            while (true)
            {
                Console.Write("Enter year: ");
                int year;
                if (int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine($"\nBooks not released in {year} by Id ASC:");
                    context.Books
                        .Where(b => b.ReleaseDate == null || b.ReleaseDate.Value.Year != year)
                        .OrderBy(b => b.Id)
                        .ToList()
                        .ForEach(b => Console.WriteLine(b.Title));
                    break;
                }
                else Console.Write("Invalid input. ");
            }
            Pause();
        }

        private static void BooksByPrice(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 2. Books by Price (price < 5 or price > 40, by Id ASC)\n");

            context.Books
                .Where(b => b.Price < 5 || b.Price > 40)
                .OrderBy(b => b.Id)
                .ToList()
                .ForEach(b => Console.WriteLine($"{b.Title} - ${b.Price:f2}"));

            Pause();
        }

        private static void GoldenBooks(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 2. Golden Books with less than 5000 copies, by Id ASC\n");

            context.Books
                .Where(b => b.Edition == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.Id)
                .ToList()
                .ForEach(b => Console.WriteLine(b.Title));

            Pause();
        }

        private static void BookTitlesByAgeRestrictionV2(BookShopContext context)
        {
            Console.WriteLine("Solution to Problem 1. Books Titles by Age Restriction v.2");
            Console.Write("Enter Age Restiction [minor/ teen/ adult]: ");
            string ageCriteria = Console.ReadLine();

            AgeRestriction ageRestrictionCriteria = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), ageCriteria, true);
            if (Enum.IsDefined(typeof(AgeRestriction), ageRestrictionCriteria))
            {
                context.Books
                    .Where(b => b.AgeRestriction == ageRestrictionCriteria)
                    .ToList()
                    .ForEach(b => Console.WriteLine(b.Title));
            }
            else Console.WriteLine("Invalid age restiction value");

            Pause();
        }

        private static void BookTitlesByAgeRestrictionV1(BookShopContext context)
        {
            Console.WriteLine("\nSolution to Problem 1. Book Titles by Age Restriction v.1");
            Console.Write("Enter Age Restiction [minor/ teen/ adult]: ");
            string ageCriteria = Console.ReadLine();

            context.Books
                .Where(b => b.AgeRestriction.ToString() == ageCriteria)
                .ToList()
                .ForEach(b => Console.WriteLine(b.Title));

            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue with the next problem");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
