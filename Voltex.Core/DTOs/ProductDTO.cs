using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Product;

namespace Voltex.Core.DTOs
{
    public record ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public List<ImageDTO> images { get; set; } = [];
    }
    public record UpdateProductDTO(int id, string Name, string Description, decimal price, int CategoryId);

    // Image DTO

    public record ImageDTO(string ImageName, int ProductId);
}
