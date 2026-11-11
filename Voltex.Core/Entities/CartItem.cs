namespace Voltex.Core.Entities
{
    public class CartItem:BaseEntity<int>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

    }
}