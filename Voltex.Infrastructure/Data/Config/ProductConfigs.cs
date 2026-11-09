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
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.OldPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.NewPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasData(
                new Product() { Id = 1, Name = "TestProducts", Description = "Test Description", OldPrice = 182.12m,NewPrice = 100.00m, CategoryId = 1 }
                );
        }
    }
}
