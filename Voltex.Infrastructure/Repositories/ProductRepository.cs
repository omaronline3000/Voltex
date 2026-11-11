using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using Voltex.Core.Entities.Product;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;
using Voltex.Core.Helper;
using Voltex.Core.Sharing;
using Voltex.Core.DTOs;
namespace Voltex.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResults<ProductDTO>> GetAllAsync(QueryParameters parameters)
        {
            IQueryable<Product> query = _context.products.AsNoTracking();

            if (parameters.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == parameters.CategoryId);
            
            if(parameters.MaxPrice.HasValue)
                query = query.Where(p => p.NewPrice <= parameters.MaxPrice);

            if (parameters.MinPrice.HasValue)
                query = query.Where(p => p.NewPrice >= parameters.MinPrice);

            // TODO: Fix it
            if (!string.IsNullOrEmpty(parameters.Search))
                query = query.Where(p => p.Name.ToLower().Contains(parameters.Search.ToLower()) || p.Description.ToLower().Contains(parameters.Search.ToLower()));

            int count = await query.CountAsync();
            query = ApplySort(query, parameters);

            var ProductDTOquery = query.Select(p => new ProductDTO()
            {
                Name = p.Name,
                CategoryName = p.category.CategoryTitle,
                Description = p.Description,
                images = (List<ImageDTO>)p.images.Select(i => new ImageDTO() { ImageName = i.ImageName , ProductId = i.ProductId}),
                NewPrice = p.NewPrice,
                OldPrice = p.OldPrice
            });

            

            IReadOnlyList<ProductDTO>? products = await ProductDTOquery.Skip(parameters.PageSize * (parameters.PageIndx - 1))
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResults<ProductDTO>()
            {
                Items = products,
                PageNumber = parameters.PageIndx,
                PageSize = parameters.PageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling((decimal)count / parameters.PageSize)
            };
        }

        public async Task<PagedResults<ProductDTO>> GetAllAsync(QueryParameters parameters, params Expression<Func<Product, object>>[] includes)
        {
            IQueryable<Product> query = _context.products.AsNoTracking();

            if (parameters.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == parameters.CategoryId);

            if (parameters.MaxPrice.HasValue)
                query = query.Where(p => p.NewPrice <= parameters.MaxPrice);

            if (parameters.MinPrice.HasValue)
                query = query.Where(p => p.NewPrice >= parameters.MinPrice);

            if (!string.IsNullOrEmpty(parameters.Search))
                query = query.Where(p => p.Name.Contains(parameters.Search) || p.Description.Contains(parameters.Search));

            int count = await query.CountAsync();

            query = ApplySort(query, parameters);


            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var ProductDTOquery = query.Select(p => new ProductDTO()
            {
                Name = p.Name,
                CategoryName = p.category.CategoryTitle,
                Description = p.Description,
                images = (List<ImageDTO>)p.images.Select(i => new ImageDTO() { ImageName = i.ImageName, ProductId = i.ProductId }),
                NewPrice = p.NewPrice,
                OldPrice = p.OldPrice
            });

            ProductDTOquery = ProductDTOquery.Skip(parameters.PageSize * (parameters.PageIndx - 1))
                .Take(parameters.PageSize);

            IReadOnlyList<ProductDTO>? products = await ProductDTOquery.ToListAsync();

            return new PagedResults<ProductDTO>()
            {
                Items = products,
                PageNumber = parameters.PageIndx,
                PageSize = parameters.PageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling((decimal)count / parameters.PageSize)
            };
        }

        private IQueryable<Product> ApplySort(IQueryable<Product> query, QueryParameters parameters)
        {
            bool decending = parameters.SortDirection!.Equals(SortDirections.DEC, StringComparison.OrdinalIgnoreCase);
            IOrderedQueryable<Product> Orderedquery = parameters.SortBy?.ToLower() switch
            {
                "price" => decending ? query.OrderByDescending(p => p.NewPrice) : query.OrderBy(p => p.NewPrice),
                "name" => decending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                _ => query.OrderBy(p => p.Id)
            };
            return Orderedquery.ThenBy(p => p.Id);
        }
    }
}
