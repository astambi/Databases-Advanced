namespace BookCatalog.Models
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public Book()
        {
            this.Genres = new HashSet<Genre>();
        }

        public int Id { get; set; }
        public string RefNumber { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePublished { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }        
    }
}
