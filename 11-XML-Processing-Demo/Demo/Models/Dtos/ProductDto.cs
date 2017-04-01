namespace Demo.Models.Dtos
{
    using Newtonsoft.Json;

    public class ProductDto
    {
        public string Name { get; set; }

        [JsonProperty("Price")]
        public decimal Cost { get; set; }

        public int StockQuantity { get; set; }

        public override string ToString()
        {
            return $"{Name} @ {Cost:f2} x {StockQuantity}";
        }
    }
}
