using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Interfaces;
using Voltex.Core.Sharing;
using Voltex.Infrastructure.Data;
using Voltex.Infrastructure.Repositories;
using Voltex.Infrastructure.Services;

namespace Voltex.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection InfrastuctureConfiguration(this IServiceCollection services, IConfiguration configure)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<EmailSettings>(configure.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();
            
            var webRoot = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot");
            Directory.CreateDirectory(webRoot);
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(webRoot));



            // Register Storage and Caching
            services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                return ConnectionMultiplexer.Connect(configure.GetConnectionString("redis")!);
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configure.GetConnectionString("Postgres"));
            });

            return services;
        } 
    }
}
