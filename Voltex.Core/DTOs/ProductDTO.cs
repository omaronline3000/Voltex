using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.DTOs
{
    public record ProductDTO(string Name, string Description, decimal price, int CategoryId);
    public record UpdateProductDTO(int id, string Name, string Description, decimal price, int CategoryId);
}
