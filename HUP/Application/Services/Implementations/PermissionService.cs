using HUP.Application.Services.Interfaces;
using HUP.Core.Interfaces;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations;

public class PermissionService : IPermissionService
{
    private readonly ICacheService _cache;
    private readonly IPermissionRepository _repository;

    public PermissionService(ICacheService cache, IPermissionRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }
    public async Task<List<string>> SetUserPermissionsAsync(Guid userId,  Guid roleId)
    {
        var id = userId.ToString();
        var key = $"user:{id}:permissions";
        
        var cachedPermissions = await _cache.GetAsync<List<string>>(key);
        if (cachedPermissions != null)
            return cachedPermissions;
        
        var dbPermissions = await _repository.GetAllPermissionsForRole(roleId);
        await _cache.SetAsync(key, dbPermissions, 60);
        return dbPermissions;
    }
}