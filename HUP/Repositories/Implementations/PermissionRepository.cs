using HUP.Core.Entities.Permissions;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations;

public class PermissionRepository : IPermissionRepository
{
    private readonly HUPDbContext _context;
    public PermissionRepository(HUPDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Permission>> GetAllPermissions()
    {
        return await _context.Permissions.ToListAsync();
    }

    public async Task<IEnumerable<RolePermission>> GetAllPermissionsForRole(Guid roleId)
    {
        return await  _context.RolePermissions.Where(r => r.RoleId == roleId).ToListAsync();
    }

    public async Task AddPermission(Permission permission)
    {
        await _context.Permissions.AddAsync(permission);
    }

    public void UpdatePermission(Permission permission)
    { 
        _context.Permissions.Update(permission);
    }

    public void DeletePermission(Guid id)
    {
        var entity = _context.Permissions.FirstOrDefault(p => p.Id == id);
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