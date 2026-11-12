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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConnectionMultiplexer _connect;
        private readonly IEmailService _emailService;

        public ICategoryRepository categoryRepository { get; }

        public IProductRepository productRepository  {get;}

        public IImageRepository imageRepository { get; }

        public ICartRepository cartRepository { get; }

        public IAuthenticationRepository authentication { get; }
        public UnitOfWork(AppDbContext context , IConnectionMultiplexer connection , UserManager<AppUser> userManager, SignInManager<AppUser> signInManager , IEmailService emailService)
        {
            _context = context;
            _connect = connection;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;

            categoryRepository = new CategoryRepository(_context);
            productRepository = new ProductRepository(_context);
            imageRepository = new ImageRepository(_context);
            cartRepository = new CartRepository(_connect);
            authentication = new AuthenticationRepository(_userManager,_signInManager,_emailService);
        }

    }
}
