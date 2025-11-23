namespace HUP.Application.Services.Interfaces;

public interface IPermissionService
{ 
    // In redis: permissions key = $"user:{userId}:permissions"
    Task<List<string>> SetUserPermissionsAsync(Guid userId, Guid roleId);
}