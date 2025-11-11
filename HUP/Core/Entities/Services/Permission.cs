using HUP.Core.Entities.Shared;
using HUP.Core.Entities.UserModels;

namespace HUP.Core.Entities.ServiceModels
{
    public class Permission : BaseEntity
    {
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }

        public virtual ICollection<RolePagePermission> RolePagePermissions { get; set; }
        public virtual ICollection<UserPagePermission> UserPagePermissions { get; set; }
    }
}
