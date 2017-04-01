namespace BookCatalog.Data
{
    public class Init
    {
        public static void InitializeDB()
        {
            BookCatalogContext context = new BookCatalogContext();
            context.Database.Initialize(true);
        }
    }
}
