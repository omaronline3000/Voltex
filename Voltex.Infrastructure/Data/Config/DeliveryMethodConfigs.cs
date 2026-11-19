using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Order;

namespace Voltex.Infrastructure.Data.Config
{
    public class DeliveryMethodConfigs : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price).HasColumnType("decimal(18,2)");
            builder.HasData(new List<DeliveryMethod>()
            {
                new(){Id = 1 , Name = "DHL" , DeliveryTime = "3" , Description = "The Fastest Method" , Price = 500.00m},
                new(){Id = 2 , Name = "Egyption Post" , DeliveryTime = "5" , Description = "Mid Speed Method" , Price = 350.00m},
                new(){Id = 3 , Name = "OASP" , DeliveryTime = "7" , Description = "Slow Method" , Price = 150.00m}
            });
        }
    }
}
