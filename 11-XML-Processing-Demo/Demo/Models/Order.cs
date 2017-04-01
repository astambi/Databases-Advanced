namespace Demo.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public Order()
        {
            //this.Products = new HashSet<Product>();
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        //[Required]
        public int? ClientId { get; set; }

        [JsonIgnore] // use to avoid circular references with Json serialization
        public virtual Client Client { get; set; }

        [JsonIgnore] // use to avoid circular references with Json serialization
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
