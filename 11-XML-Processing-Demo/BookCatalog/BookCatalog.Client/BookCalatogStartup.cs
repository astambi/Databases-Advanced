namespace BookCatalog.Client
{
    using Business;
    using Data;
    using Data.DTO;
    using System.Collections.Generic;

    class BookCalatogStartup
    {
        static void Main(string[] args)
        {
            /* EF Configuration:
             * Install EF only in Data, do NOT install EF in Client
             * To be able to access EF SQLServer from Client
             * Copy EntityFramework.SqlServer.dll 
             * From Data/bin/Debug To Client/bin/Debug
             * Add References: 
             * - Business to Data
             * - Client to Business, Data & Models
             * - Data to Models
             */

            Init.InitializeDB();
            ImportBooks();
        }

        private static void ImportBooks()
        {
            ICollection<BookDTO> books = XmlImport.ImportBooks("../../catalog/books.xml");

            foreach (BookDTO book in books)
            {
                BookDML.AddBook(book);
            }
        }
    }
}
