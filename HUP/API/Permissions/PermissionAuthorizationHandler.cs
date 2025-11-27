using System.Security.Claims;
using HUP.Application.Services.Interfaces;
using HUP.Core.Interfaces;
using HUP.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HUP.API.Permissions;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ICacheService _cache;
    private readonly IUserRepository _repository;
    private readonly IPermissionService _permissionService;

    public PermissionAuthorizationHandler(ICacheService cacheService, IUserRepository userRepository,  IPermissionService permissionService)
    {
        _cache = cacheService;
        _repository = userRepository;
        _permissionService = permissionService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // get user id from claims
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            context.Fail();
            return;
        }
        //user:{id}:permissions
        //$"user:{id}:role"
        string permissionKey = $"user:{userId}:permissions",
            roleKey = $"user:{userId}:role";
        
        // get cached permissions and role id
        var permissions = await _cache.GetAsync<string[]>(permissionKey);
        var role =  await _cache.GetAsync<string>(roleKey);
        
        // role cached value is null (expired)
        // set the value in cache
        if (role == null)
        {
            var user = await _repository.GetByIdAsync(Guid.Parse(userId));
            role = user.RoleId.ToString();
            await _cache.SetAsync(roleKey, role, 2);
        }
        // if permissions value == null (expired)
        // set the permissions in cache and return them 
        if (permissions == null)
        {
            permissions = (await _permissionService.SetUserPermissionsAsync(Guid.Parse(userId), Guid.Parse(role))).ToArray();
        }
        // get role id from token
        var tokenRoleId = context.User.FindFirst("roleId")?.Value;
        // check if permissions contains the required permission and role id matches
        if (permissions.Contains(requirement.Permission) && role == tokenRoleId)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}