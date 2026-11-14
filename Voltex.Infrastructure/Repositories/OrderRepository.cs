using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using Voltex.Core.Entities.Order;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;

namespace Voltex.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order entity)
        {
            await _context.orders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Order>> AddRangeAsync(IEnumerable<Order> entities)
        {
            await _context.orders.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(Order entity)
        {
             _context.orders.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync(string email) { return await _context.orders.AsNoTracking().Where(o => o.ClientEmail == email).ToListAsync(); }
        

        public async Task<IReadOnlyList<Order>> GetAllAsync(string email, params Expression<Func<Order, object>>[] includes)
        {
            IQueryable<Order> query = _context.orders.AsNoTracking();
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            var list = await query.Where(o => o.ClientEmail == email).ToListAsync();
            return list;
        }

        public async Task<Order?> GetByIdAsync(int id, string email)
        {
            return await _context.orders.AsNoTracking().FirstOrDefaultAsync(o => o.ClientEmail == email && o.Id == id);
        }

        public async Task<Order?> GetByIdAsync(int id, string email, params Expression<Func<Order, object>>[] includes)
        {
            IQueryable<Order> query = _context.orders.AsNoTracking();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var list = await query.FirstOrDefaultAsync(o => o.ClientEmail == email && o.Id == id)!;
            return list;
        }

        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
