namespace BookCatalog.Data
{
    using System;

    public class Init
    {
        public static void InitializeDB()
        {
            Console.WriteLine("Initializing Database [BookCatalog]");

            using (BookCatalogContext context = new BookCatalogContext())
            {
                context.Database.Initialize(true);
            }
        }
    }
}
