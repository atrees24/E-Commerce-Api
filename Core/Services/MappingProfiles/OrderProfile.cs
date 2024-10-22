using AutoMapper;
using Domain.Models;
using OrderAddress= Domain.Models.OrderEntities.Address;
using Shared.OrderModels;
using Domain.Models.OrderEntities;
namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddress, AddressDTO>().ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>()
               .ForMember(d => d.ProductId,
                            options => options.
                            MapFrom(s => s.Product.ProductId))
               .ForMember(d => d.PictureUrl,
                            options => options.MapFrom(s => s.Product.PictureUrl))
               .ForMember(d => d.ProductName,
               options => options.MapFrom(s => s.Product.ProductName));

            CreateMap<Order, OrderResult>().
                ForMember(d => d.PaymentStatus,
options => options.MapFrom(s => s.ToString())).
                ForMember(d => d.DelveryMethod,         
options => options.MapFrom(s => s.DelveryMethod.ShortName)).
                ForMember(d => d.Total,
options => options.MapFrom(s => s.Subtotal + s.DelveryMethod.Price));

            CreateMap<DelveryMethod, DelveryMethodResult>();
            CreateMap<AddressDTO, Domain.Models.Address>().ReverseMap();
        }
    }
}
