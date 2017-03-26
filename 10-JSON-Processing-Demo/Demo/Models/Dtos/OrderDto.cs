namespace Demo.Models.Dtos
{
    public class OrderDTO
    {
        public string ClientName { get; set; }
        public decimal OrderTotal { get; set; }
        public override string ToString()
        {
            return $"{ClientName} - {OrderTotal:f2}";
        }
    }
}
