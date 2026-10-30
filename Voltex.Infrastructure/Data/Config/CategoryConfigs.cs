using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Product;

namespace Voltex.Infrastructure.Data.Config
{
    public class CategoryConfigs : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.CategoryTitle).IsRequired().HasMaxLength(30);
            builder.HasData(
                new Category() { Id = 1, CategoryTitle = "Test", Description = "Test Description" }
                );
        }
    }
}
