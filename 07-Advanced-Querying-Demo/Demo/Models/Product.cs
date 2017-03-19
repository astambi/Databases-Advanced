namespace Demo.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        //[ConcurrencyCheck] // => first user wins, next user gets an exception
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
