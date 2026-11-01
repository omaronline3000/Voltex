using AutoMapper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;

namespace Voltex.API.Mappers
{
    public class CategoryMapper:Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
