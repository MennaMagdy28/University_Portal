using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.Permissions
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}