namespace Demo.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            //this.Orders = new HashSet<Order>();
            this.ProductStocks = new HashSet<ProductStock>();
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        [Required]
        //[ConcurrencyCheck] // => first user wins, next user gets an exception
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        [JsonIgnore] // use to avoid circular references with Json serialization
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        [JsonIgnore] // use to avoid circular references with Json serialization
        public virtual ICollection<ProductStock> ProductStocks { get; set; }
    }
}
