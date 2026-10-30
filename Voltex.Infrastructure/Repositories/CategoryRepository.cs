using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities.Product;
using Voltex.Core.Interfaces;
using Voltex.Infrastructure.Data;

namespace Voltex.Infrastructure.Repositories
{
    public class CategoryRepository:BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(AppDbContext context): base(context)
        {
        }


    }
}
