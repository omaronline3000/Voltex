using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Product;

namespace Voltex.Infrastructure.Data.Config
{
    public class ImageConfigs : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(
                new Image() { Id = 1, ImageName = "Test1", ProductId = 1 },
                new Image() { Id = 2, ImageName = "Test2", ProductId = 1 },
                new Image() { Id = 3, ImageName = "Test3", ProductId = 1 }
                );
        }
    }
}
