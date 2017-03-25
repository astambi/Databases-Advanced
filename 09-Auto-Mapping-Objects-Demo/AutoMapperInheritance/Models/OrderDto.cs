namespace AutoMapperInheritance.Models
{
    public abstract class OrderDto
    {
        public string TrackingInfo { get; set; }
    }

    public class OnlineOrderDto : OrderDto
    {
        public string BrowserVersion { get; set; }
    }

    public class MailOrderDto : OrderDto
    {
        public int DeliveryAgentId { get; set; }
    }
}
