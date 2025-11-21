using System.Security.Claims;
using HUP.Core.Entities.Permissions;
using HUP.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HUP.API.Permissions;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ICacheService _cache;

    public PermissionAuthorizationHandler(ICacheService cacheService)
    {
        _cache = cacheService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            context.Fail();
            return;
        }
        var permissions = await _cache.GetAsync<Permission[]>(userId);
        var permissionStrings = permissions.Select(p => p.Name).ToArray();
        
        if (permissionStrings.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}