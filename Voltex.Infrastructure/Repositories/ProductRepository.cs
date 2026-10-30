using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Product;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;

namespace Voltex.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

    }
}
