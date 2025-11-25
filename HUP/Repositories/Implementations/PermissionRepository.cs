using HUP.Core.Entities.Permissions;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations;

public class PermissionRepository : IPermissionRepository
{
    private readonly HupDbContext _context;
    public PermissionRepository(HupDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Permission>> GetAllPermissions()
    {
        return await _context.Permissions.ToListAsync();
    }

    public async Task<List<string>> GetAllPermissionsForRole(Guid roleId)
    {
        var permissionNames = await _context.RolePermissions.Where(r => r.RoleId == roleId)
            .Select(r => r.Permission.Name).ToListAsync();
        return permissionNames;
    }

    public async Task AddPermission(Permission permission)
    {
        await _context.Permissions.AddAsync(permission);
    }

    public void UpdatePermission(Permission permission)
    {
        _context.Permissions.Update(permission);
    }

    public async Task DeletePermissionAsync(Guid id)
    {
        var entity = await _context.Permissions.FindAsync(id);
        
        if (entity != null)
            _context.Permissions.Remove(entity);
    }

    public async Task AddRolePermission(Guid permissionId, Guid roleId)
    {
        RolePermission relation = new RolePermission();
        relation.RoleId = roleId;
        relation.PermissionId = permissionId;
        await _context.RolePermissions.AddAsync(relation);
    }

    public void DeleteRolePermission(Guid permissionId, Guid roleId)
    {
        var relation = _context.RolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId
                                                                     && rp.RoleId == roleId);
        _context.RolePermissions.Remove(relation);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}