namespace BookCatalog.Data.DTO
{
    using System;

    public class BookDTO
    {
        public string RefNumber { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePublished { get; set; }
        public string Description { get; set; }


    }
}
