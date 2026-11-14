using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Voltex.Core.Entities;
using Voltex.Core.Entities.Order;
using Voltex.Core.Entities.Product;

namespace Voltex.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<Category> categories { get; set; }
        public virtual DbSet<Image> images { get; set; }
        public virtual DbSet<Address> addresses { get;set; }
        public virtual DbSet<Order> orders { get; set; }
        public virtual DbSet<OrderItem> orderItems { get; set; }
        public virtual DbSet<DeliveryMethod> deliveryMethods { get; set; }
        //public virtual DbSet<ShippingData> shippingData { get; set; }
    }
}
