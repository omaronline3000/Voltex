using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Voltex.Core.Entities.Product
{
    public class Product:BaseEntity<int>
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }

        //Nav
        public Category category { get; set; }
        public ICollection<Image> images { get; set; }
    }
}
