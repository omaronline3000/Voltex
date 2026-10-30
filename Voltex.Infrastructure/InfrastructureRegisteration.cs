using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;
using Voltex.Infrastructure.Repositories;

namespace Voltex.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection InfrastuctureConfiguration(this IServiceCollection services, IConfiguration configure)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configure.GetConnectionString("Postgres"));
            });
            return services;
        } 
    }
}
