using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Order;

namespace Voltex.Infrastructure.Data.Config
{
    public class ShippingDataConfigs : IEntityTypeConfiguration<ShippingData>
    {
        public void Configure(EntityTypeBuilder<ShippingData> builder)
        {
            builder.HasOne(s => s.Order).WithOne(o => o.ShippingData).HasForeignKey<ShippingData>(s => s.OrderId);
        }
    }
}
