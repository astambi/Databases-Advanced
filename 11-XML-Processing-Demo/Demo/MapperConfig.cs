namespace Demo
{
    using AutoMapper;
    using Models;
    using Models.Dtos;
    using System.Linq;

    public static class MapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(action =>
            {
                action.CreateMap<Product, ProductDto>()
                    .ForMember(
                        dto => dto.StockQuantity,
                        configExpression => configExpression.MapFrom(src => src.ProductStocks.Sum(p => p.Quantity)));
                action.CreateMap<Order, OrderDto>()
                    .ForMember(
                        dto => dto.OrderTotal,
                        configExpression => configExpression.MapFrom(src => src.OrderProducts.Sum(op => op.Product.Cost * op.Quantity)));
            });
        }
    }
}
