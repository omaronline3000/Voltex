using AutoMapper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;

namespace Voltex.API.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();
        }
    }
}
