namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Car
    {
        public Car()
        {
            this.Parts = new HashSet<Part>();
            this.Sales = new HashSet<Sale>(); // added
        }

        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
        public virtual ICollection<Sale> Sales { get; set; } // added
    }
}
