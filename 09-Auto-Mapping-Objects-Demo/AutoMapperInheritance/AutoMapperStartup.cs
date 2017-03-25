namespace AutoMapperInheritance
{
    using AutoMapper;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class AutoMapperStartup
    {
        static void Main(string[] args)
        {
            // Install AutoMapper NuGet

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>()
                    .Include<OnlineOrder, OnlineOrderDto>()
                    .Include<MailOrder, MailOrderDto>();
                cfg.CreateMap<OnlineOrder, OnlineOrderDto>();
                cfg.CreateMap<MailOrder, MailOrderDto>();
            });

            Order order = new OnlineOrder()
            {
                TrackingInfo = "dfasfxzv1z2x313z2",
                BrowserVersion = "Mozilla"
            };
            var orderDto = Mapper.Map<OrderDto>(order);
        }
    }
}
