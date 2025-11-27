using Microsoft.AspNetCore.Authorization;

namespace HUP.API.Permissions;

// This class implements IAuthorizationRequirement and holds the required permission string
// [Authorize(Policy = "PermissionRequirement")] for using this requirement in controllers or actions.
public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission)
    {
        // Set the required permission
        Permission = permission;
    }
}