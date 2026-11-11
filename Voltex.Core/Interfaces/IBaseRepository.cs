using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Voltex.Core.Entities.Product;
using Voltex.Core.Sharing;

namespace Voltex.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(int id , params Expression<Func<T, object>>[] includes);

        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);


    }
}
