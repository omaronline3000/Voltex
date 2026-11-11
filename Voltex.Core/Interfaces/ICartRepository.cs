using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities;

namespace Voltex.Core.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string id);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(string id);
    }
}
