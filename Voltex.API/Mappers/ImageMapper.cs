using AutoMapper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;

namespace Voltex.API.Mappers
{
    public class ImageMapper:Profile
    {
        public ImageMapper()
        {
            CreateMap<Image, ImageDTO>()
                .ReverseMap();
        }
    }
}
