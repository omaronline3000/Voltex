using AutoMapper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;

namespace Voltex.API.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
                CreateMap<Product, ProductDTO>()
                    .ForMember(p => p.CategoryName, optoins => optoins.MapFrom(p => p.category.CategoryTitle))
                    .ReverseMap();
            CreateMap<AddProductDTO, Product>()
                .ForMember(p => p.images, options => options.Ignore())
                .ReverseMap();
            CreateMap<UpdateProductDTO, Product>()
                    .ReverseMap();        
            
        }
    }
}
