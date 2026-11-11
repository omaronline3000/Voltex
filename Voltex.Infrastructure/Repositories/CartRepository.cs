using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Voltex.Core.Entities;
using Voltex.Core.Interfaces;

namespace Voltex.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;
        public CartRepository(IConnectionMultiplexer connect)
        {
            _database = connect.GetDatabase();
        }

        public async Task<bool> DeleteCartAsync(string id)
        {
            return await _database.KeyDeleteAsync(id); 
        }

        public async Task<Cart> GetCartAsync(string id)
        {
            var cart = await _database.StringGetAsync(id);
            if (!cart.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<Cart>(json: cart);
            }
            return null;
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            var res = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize<Cart>(cart), TimeSpan.FromDays(3));
            if (res)
            {
                var cachedCart =  await _database.StringGetAsync(cart.Id);
                return JsonSerializer.Deserialize<Cart>(json: cachedCart);
            }
            return null;
        }
    }
}
