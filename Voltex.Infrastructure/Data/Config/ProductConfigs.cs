using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Product;
namespace Voltex.Infrastructure.Data.Config
{
    public class ProductConfigs : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasData(
                new Product() { Id = 1, ProductName = "TestProducts", Description = "Test Description", Price = 182.12m, CategoryId = 1 }
                );
        }
    }
}
