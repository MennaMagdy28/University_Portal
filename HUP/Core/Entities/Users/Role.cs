using HUP.Core.Entities.ServiceModels;
using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.UserModels
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int CreatedBy { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePagePermission> RolePagePermissions { get; set; }
    }
}
