using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Order;

namespace Voltex.Core.DTOs
{
    public record OrderDTO
    {
        public int deliveryMethodId { get; set; }
        public string CartId { get; set; }
        public ShipDataDTO shipdata { get; set; }
    }
    public record ShipDataDTO
    {

    }
}
