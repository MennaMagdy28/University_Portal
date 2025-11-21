using HUP.Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace HUP.Repositories.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cache;

        public CacheService(IConnectionMultiplexer redis)
        {
            _cache = redis.GetDatabase();
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            RedisValue val = await _cache.StringGetAsync(key);
            if (val.IsNullOrEmpty)
            {
                return default;
            } 
            return JsonSerializer.Deserialize<T>(val.ToString());

        }

        public Task RemoveAsync(string key)
        {
            return _cache.KeyDeleteAsync(key);
        }

        public Task SetAsync<T>(string key, T value, int expirationInMinutes)
        {
            string jsonData = JsonSerializer.Serialize(value);
            return _cache.StringSetAsync(key, jsonData, TimeSpan.FromMinutes(expirationInMinutes));
        }
    }
}
