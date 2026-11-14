using System.ComponentModel.DataAnnotations.Schema;

namespace Voltex.Core.Entities.Order
{
    public class OrderItem:BaseEntity<int>
    {
        public OrderItem()
        {
            
        }

        public OrderItem(int productItemId, string mainImage, string productName, int quantity, decimal price)
        {
            ProductItemId = productItemId;
            MainImage = mainImage;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }

        public int ProductItemId { get; set; }
        public string MainImage  { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(OrderId))]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

    }
}