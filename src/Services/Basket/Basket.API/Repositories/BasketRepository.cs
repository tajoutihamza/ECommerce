using Basket.API.Entities;
using Basket.API.Repositories.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache cache)
        {
            _redisCache= cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task deleteBasket(string username)
        {
            await _redisCache.RemoveAsync(username);
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redisCache.GetStringAsync(username);
            if(String.IsNullOrEmpty(basket)) return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);

        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            await _redisCache.SetStringAsync(cart.UserName,JsonConvert.SerializeObject(cart));
            return await GetBasket(cart.UserName);
        }
    }
}
