using Microsoft.EntityFrameworkCore.Migrations;
using StackExchange.Redis;
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
        private readonly IConnectionMultiplexer _connect;
        public ICategoryRepository categoryRepository { get; }

        public IProductRepository productRepository  {get;}

        public IImageRepository imageRepository { get; }

        public ICartRepository cartRepository { get; }

        public UnitOfWork(AppDbContext context , IConnectionMultiplexer connection)
        {
            _context = context;
            _connect = connection;
            categoryRepository = new CategoryRepository(_context);
            productRepository = new ProductRepository(_context);
            imageRepository = new ImageRepository(_context);
            cartRepository = new CartRepository(_connect);
        }

    }
}
