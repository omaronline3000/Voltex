using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.Entities.Product
{
    public class Category:BaseEntity<int>
    {
        public string CategoryTitle { get; set; }
        public string Description { get; set; }

        //Nav
        public virtual ICollection<Product> products { get; set; }
    }
}
