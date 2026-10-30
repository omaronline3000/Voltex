using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Product;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;

namespace Voltex.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
