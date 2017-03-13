namespace BookShopSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum EditionType     // Step 3
    { Normal, Promo, Gold }

    public enum AgeRestriction  // Step 4
    { Minor, Teen, Adult }

    public class Book           // Step 3
    {
        private ICollection<Category> categories;
        private ICollection<Book> relatedBooks;     // Step 6

        public Book()
        {
            this.categories = new HashSet<Category>();
            this.relatedBooks = new HashSet<Book>(); // Steo 6
        }

        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(50)]
        public string Title { get; set; }

        // optional
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public EditionType Edition { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Copies { get; set; }

        // optional
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public AgeRestriction AgeRestriction { get; set; } // Step 4

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public virtual ICollection<Book> RelatedBooks // Step 6
        {
            get { return this.relatedBooks; }
            set { this.relatedBooks = value; }
        }
    }
}
