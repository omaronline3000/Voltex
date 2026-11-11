using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Voltex.Core.Helper;
using Voltex.Core.Entities.Product;
using Voltex.Core.Sharing;
using Voltex.Core.DTOs;
namespace Voltex.Core.Interfaces
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<PagedResults<ProductDTO>> GetAllAsync(QueryParameters parameters);
        Task<PagedResults<ProductDTO>> GetAllAsync(QueryParameters parameters, params Expression<Func<Product, object>>[] includes);
    }
}
