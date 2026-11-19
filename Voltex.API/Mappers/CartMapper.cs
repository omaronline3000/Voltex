using AutoMapper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities;
namespace Voltex.API.Mappers
{
    public class CartMapper:Profile
    {
        public CartMapper()
        {
            CreateMap<CartDTO, Cart>().ReverseMap();
        }
    }
}
