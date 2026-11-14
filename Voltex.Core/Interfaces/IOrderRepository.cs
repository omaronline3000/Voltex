using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using Voltex.Core.Entities.Order;

namespace Voltex.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetAllAsync(string email);
        Task<IReadOnlyList<Order>> GetAllAsync(string email,params Expression<Func<Order, object>>[] includes);

        Task<Order?> GetByIdAsync(int id, string email);
        Task<Order?> GetByIdAsync(int id,string email, params Expression<Func<Order, object>>[] includes);

        Task<Order> AddAsync(Order entity);
        Task<IEnumerable<Order>> AddRangeAsync(IEnumerable<Order> entities);
        Task UpdateAsync(Order entity);
        Task DeleteAsync(Order entity);
    }
}
