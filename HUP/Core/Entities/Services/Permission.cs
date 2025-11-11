using HUP.Core.Models.Shared;
using HUP.Core.Models.UserModels;

namespace HUP.Core.Models.ServiceModels
{
    public class Permission : BaseEntity
    {
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }

        public virtual ICollection<RolePagePermission> RolePagePermissions { get; set; }
        public virtual ICollection<UserPagePermission> UserPagePermissions { get; set; }
    }
}
