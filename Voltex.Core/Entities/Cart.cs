using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.Entities
{
    public class Cart:BaseEntity<string>
    {
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public ICollection<CartItem> Items { get; set; } = [];
    }
}
