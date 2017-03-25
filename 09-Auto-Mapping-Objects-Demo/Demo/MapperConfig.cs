namespace Demo
{
    using AutoMapper;
    using Models;
    using System.Linq;
    using static DemoStartup;

    public static class MapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>()
                    .ForMember(
                        dto => dto.StockQuantity,
                        opt => opt.MapFrom(src => src.ProductStocks.Sum(p => p.Quantity)));
                cfg.CreateMap<Order, OrderDTO>()
                    .ForMember(
                        dto => dto.OrderTotal,
                        opt => opt.MapFrom(src => src.OrderProducts.Sum(op => op.Product.Cost * op.Quantity)));
            });
        }
    }
}
