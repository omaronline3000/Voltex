using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Product;

namespace Voltex.Core.DTOs
{
    public record ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<ImageDTO> images { get; set; } = [];
    }

    public record AddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Images { set; get; }
    }


    public record UpdateProductDTO : AddProductDTO
    {
    }


    // Image DTO

    public record ImageDTO {
        public string ImageName { get; set; }
       public int ProductId { get; set; }
    }
}
