using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities;

namespace Voltex.Infrastructure.Data.Config
{
    public class AddressConfigs : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasOne(a => a.AppUser)
                 .WithOne(u => u.Address)
                 .HasForeignKey<Address>(a => a.AppUserId);
            builder.HasIndex(p => p.AppUserId).IsUnique();

        }
    }
}
