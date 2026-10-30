using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Voltex.Core.Entities.Product
{
    public class Image:BaseEntity<int>
    {
        public string ImageName { get; set; }

        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

        //Nav
        public virtual Product product { get; set; }

    }
}
