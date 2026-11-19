using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities;

namespace Voltex.Core.DTOs
{
    public record CartDTO
    {
        public string? Id { get; set; }
        public ICollection<CartItem> Items { get; set; } = [];
    }

}
