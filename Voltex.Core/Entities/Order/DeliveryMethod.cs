namespace Voltex.Core.Entities.Order
{
    public class DeliveryMethod:BaseEntity<int>
    {
        public DeliveryMethod()
        {
            
        }

        public DeliveryMethod(string name, string description, string deliveryTime, decimal price)
        {
            Name = name;
            Description = description;
            DeliveryTime = deliveryTime;
            Price = price;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }

        public virtual IReadOnlyList<Order> orders { get; set; }
    }
}