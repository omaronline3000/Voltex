using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Voltex.Core.Entities.Order;

namespace Voltex.Core.DTOs
{

    public record OrderDTO
    {
        public string OrderStatus { get; set; }
        public string ClientEmail { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public ShippingData ShippingData { get; set; }
        public string DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItemDTO> orderItems { get; set; }
    }
    public record AddOrderDTO
    {
        public int deliveryMethodId { get; set; }
        public string CartId { get; set; }
        public ShipDataDTO shipdata { get; set; }
    }

    public record ShipDataDTO
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
    }
    public record OrderItemDTO
    {
        public int ProductItemId { get; set; }
        public string MainImage { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
