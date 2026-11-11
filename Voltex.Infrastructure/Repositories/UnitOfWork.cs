using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Voltex.Core.Entities;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;

namespace Voltex.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConnectionMultiplexer _connect;
        public ICategoryRepository categoryRepository { get; }

        public IProductRepository productRepository  {get;}

        public IImageRepository imageRepository { get; }

        public ICartRepository cartRepository { get; }

        public IAuthentication authentication { get; }

        public UnitOfWork(AppDbContext context , IConnectionMultiplexer connection , UserManager<AppUser> userManager)
        {
            _context = context;
            _connect = connection;
            _userManager = userManager;
            categoryRepository = new CategoryRepository(_context);
            productRepository = new ProductRepository(_context);
            imageRepository = new ImageRepository(_context);
            cartRepository = new CartRepository(_connect);
            authentication = new Authentication(_userManager);
        }

    }
}
