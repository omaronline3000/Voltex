using AutoMapper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities;
using Voltex.Infrastructure.Data.Config;

namespace Voltex.API.Mappers
{
    public class AddressMapper:Profile
    {
        public AddressMapper()
        {
            CreateMap<AddressDTO, Address>().ReverseMap();
        }
    }
}
