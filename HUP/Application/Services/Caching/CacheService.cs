using System.Text.Json;
using HUP.Core.Interfaces;
using StackExchange.Redis;
namespace HUP.Application.Services.Caching;

public class CacheService: ICacheService
{
    private readonly IDatabase _db;
    public CacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }
    
    public async Task<T?> GetAsync<T>(string key)
    {
        var redisValue = _db.StringGet(key);
        if (redisValue.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(redisValue);
    }

    public async Task SetAsync<T>(string key, T value, int expirationInMinutes)
    {
        var json  = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json, TimeSpan.FromMinutes(expirationInMinutes));
        
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}