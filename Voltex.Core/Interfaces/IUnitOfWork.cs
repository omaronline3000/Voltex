using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository categoryRepository { get;}
        public IProductRepository productRepository { get; }
        public IImageRepository imageRepository { get;}


    }
}
