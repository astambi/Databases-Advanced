namespace BookCatalog.Business
{
    using Data.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlImport
    {
        public static ICollection<BookDTO> ImportBooks(string fileName)
        {
            XDocument xmlDoc = XDocument.Load(fileName);
            List<BookDTO> books = xmlDoc.Root.Elements()
                                .Select(ParseBook)
                                .ToList();
            return books;
        }
        public static BookDTO ParseBook(XElement book)
        {
            BookDTO bookDto = new BookDTO
            {
                RefNumber = book.Attribute("id").Value,
                Author = book.Element("author").Value,
                Title = book.Element("title").Value,
                Genre = book.Element("genre").Value,
                Price = decimal.Parse(book.Element("price").Value),
                DatePublished = DateTime.Parse(book.Element("publish_date").Value),
                Description = book.Element("description").Value
            };

            return bookDto;
        }
    }
}
