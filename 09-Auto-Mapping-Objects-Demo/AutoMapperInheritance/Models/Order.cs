namespace AutoMapperInheritance.Models
{
    public abstract class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string TrackingInfo { get; set; }
    }

    public class OnlineOrder : Order
    {
        public string UserMachine { get; set; }
        public string BrowserVersion { get; set; }
    }

    public class MailOrder : Order
    {
        public string Address { get; set; }
        public string Country { get; set; }
        public int DeliveryAgentId { get; set; }
    }
}
