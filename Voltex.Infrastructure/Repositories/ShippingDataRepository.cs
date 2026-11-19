using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Order;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;

namespace Voltex.Infrastructure.Repositories
{
    public class ShippingDataRepository : BaseRepository<ShippingData>, IShippingDataRepository
    {
        public ShippingDataRepository(AppDbContext context) : base(context)
        {
        }

    }
}
