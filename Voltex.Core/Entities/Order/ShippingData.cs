using System.ComponentModel.DataAnnotations.Schema;

namespace Voltex.Core.Entities.Order
{
    public class ShippingData:BaseEntity<int>
    {
        public ShippingData()
        {
        }

        public ShippingData(string fName, string lName, string city, string country, string zipCode, string street)
        {
            FName = fName;
            LName = lName;
            City = city;
            Country = country;
            ZipCode = zipCode;
            Street = street;
        }

        public string FName { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }

        [ForeignKey(nameof(OrderId))]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}