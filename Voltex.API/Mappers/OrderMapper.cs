using AutoMapper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Order;

namespace Voltex.API.Mappers
{
    public class OrderMapper:Profile
    {
        public OrderMapper()
        {
            CreateMap<ShipDataDTO, ShippingData>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap()
                .ForMember(o => o.DeliveryMethod , options => options
                .MapFrom(o => o.DeliveryMethod.Name));
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        }
    }
}
