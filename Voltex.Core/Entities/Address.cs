using System.ComponentModel.DataAnnotations.Schema;

namespace Voltex.Core.Entities
{
    public class Address:BaseEntity<int>
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }

        [ForeignKey(nameof(AppUserId))]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}