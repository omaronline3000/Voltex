using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Order;

namespace Voltex.Infrastructure.Data.Config
{
    public class OrderConfigs : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.DeliveryMethod).WithMany(d => d.orders).HasForeignKey(o => o.DeliverMethodId).OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            builder.Property(o => o.OrderStatus).HasConversion(status => status.ToString(), status => (Status)Enum.Parse(typeof(Status), status));
        }
    }
}
