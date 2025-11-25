using HUP.Core.Entities.Permissions;

namespace HUP.Repositories.Interfaces;

public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> GetAllPermissions();
    Task<List<string>> GetAllPermissionsForRole(Guid roleId);
    Task AddPermission(Permission permission);
    void UpdatePermission(Permission permission);
    Task DeletePermissionAsync(Guid id);
    Task AddRolePermission(Guid permissionId, Guid roleId);
    void DeleteRolePermission(Guid permissionId, Guid roleId);
    public Task SaveChangesAsync();

}