namespace HUP.Core.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, int expirationInMinutes);
        Task RemoveAsync(string key);
        
        // cache keys:
        // permissions key = $"user:{userId}:permissions"
    }
}
