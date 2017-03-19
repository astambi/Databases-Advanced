namespace Demo.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public Order()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
