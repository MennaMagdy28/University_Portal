using HUP.Core.Models.ServiceModels;
using HUP.Core.Models.Shared;

namespace HUP.Core.Models.UserModels
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
