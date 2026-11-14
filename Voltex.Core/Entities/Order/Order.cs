using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace Voltex.Core.Entities.Order
{
    public class Order:BaseEntity<int>
    {
        public Order()
        {
            
        }

        public Order(string clientEmail, decimal subTotal, ShippingData shippingData, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems)
        {
            ClientEmail = clientEmail;
            SubTotal = subTotal;
            ShippingData = shippingData;
            DeliveryMethod = deliveryMethod;
            this.orderItems = orderItems;
        }

        public Status OrderStatus { get; set; } = Status.Pending;
        public string ClientEmail { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(DeliverMethodId))]
        public int DeliverMethodId { get; set; }
        public virtual ShippingData ShippingData { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }

        public virtual IReadOnlyList<OrderItem> orderItems { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
    }
}
