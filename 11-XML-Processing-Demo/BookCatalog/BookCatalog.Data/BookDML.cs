namespace BookCatalog.Data
{
    using DTO;
    using Models;
    using System;
    using System.Linq;

    public static class BookDML
    {
        public static void AddBook(BookDTO book)
        {
            int genreId = GetGenreId(book.Genre);
            int authorId = GetAuthorId(book.Author);

            using (var context = new BookCatalogContext())
            {
                Book bookInDB = context.Books
                    .SingleOrDefault(b => b.RefNumber == book.RefNumber &&
                                          b.Title == book.Title &&
                                          b.AuthorId == authorId);

                if (bookInDB == null) // add new book
                {
                    Book newBook = new Book();
                    newBook.RefNumber = book.RefNumber;
                    newBook.Title = book.Title;
                    newBook.AuthorId = authorId;
                    newBook.Price = book.Price;
                    newBook.DatePublished = book.DatePublished;
                    newBook.Description = book.Description;
                    newBook.Genres = new[] { context.Genres.Find(genreId) };

                    context.Books.Add(newBook);
                    Console.Write($"Added book ");
                }
                else // update book
                {
                    bookInDB.Price = book.Price;
                    bookInDB.DatePublished = book.DatePublished;
                    bookInDB.Description = book.Description;
                    if (bookInDB.Genres.FirstOrDefault(g => g.Id == genreId) == null)
                    {
                        bookInDB.Genres.Add(context.Genres.Find(genreId));
                    }
                    Console.Write($"Updated book ");
                }
                Console.WriteLine($"{book.Title} ({book.Author})");
                context.SaveChanges();
            }
        }

        public static int GetGenreId(string genreName)
        {
            using (var context = new BookCatalogContext())
            {
                Genre genre = context.Genres.SingleOrDefault(g => g.Name == genreName);
                if (genre == null)
                {
                    return AddGenre(genreName);
                }
                return genre.Id;
            }
        }

        public static int AddGenre(string genreName)
        {
            using (var context = new BookCatalogContext())
            {
                Genre newGenre = new Genre { Name = genreName };
                context.Genres.Add(newGenre);
                context.SaveChanges();
                Console.WriteLine($"Added genre {genreName}");

                return newGenre.Id;
            }
        }

        public static int GetAuthorId(string fullName)
        {
            using (var context = new BookCatalogContext())
            {
                string[] names = fullName
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();

                string firstName = string.Empty;
                string lastName = string.Empty;

                if (names.Length > 0) lastName = names[0];
                if (names.Length > 1) firstName = string.Join(" ", names.Skip(1));

                Author author = context.Authors
                    .SingleOrDefault(a => a.FirstName == firstName && a.LastName == lastName);

                if (author == null)
                {
                    return AddAuthor(firstName, lastName);
                }
                return author.Id;
            }
        }

        public static int AddAuthor(string firstName, string lastName)
        {
            using (var context = new BookCatalogContext())
            {
                Author newAuthor = new Author { FirstName = firstName, LastName = lastName };
                context.Authors.Add(newAuthor);
                context.SaveChanges();
                Console.WriteLine($"Added author {firstName} {lastName}");

                return newAuthor.Id;
            }
        }
    }
}
