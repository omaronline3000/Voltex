using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
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

        public async Task<IReadOnlyList<Product>> GetAllIncludeAsync()
        {
            /*IQueryable<Product> query = _context.Set<Product>().AsNoTracking();
            query.Include(p => p.category);
            query.Include(p => p.images);
            return await query.ToListAsync();
*/
            return await _context.products
                .AsNoTracking()
                .Include(p => p.images)
                .Include(p => p.category)
                .ToListAsync();
        }
    }
}
