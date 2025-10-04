using HUP.Core.Models.UserModels;

namespace HUP.Core.Models.ServiceModels
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int CreatedBy { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePagePermission> RolePagePermissions { get; set; } = new List<RolePagePermission>();
    }
}
