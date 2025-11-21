namespace HUP.Application.Services.Interfaces;

public interface IPermissionService
{ 
    // In redis: permissions key = $"user:{userId}:permissions"
    Task<List<string>> GetUserPermissionsAsync(Guid userId, Guid roleId);
}