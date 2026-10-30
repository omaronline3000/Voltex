using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;

namespace Voltex.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICategoryRepository categoryRepository { get; }

        public IProductRepository productRepository  {get;}

        public IImageRepository imageRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            categoryRepository = new CategoryRepository(_context);
            productRepository = new ProductRepository(_context);
            imageRepository = new ImageRepository(_context);
        }

    }
}
