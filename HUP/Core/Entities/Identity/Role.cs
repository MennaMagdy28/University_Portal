namespace HUP.Core.Entities.Identity
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int CreatedBy { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePagePermission> RolePagePermissions { get; set; } = new List<RolePagePermission>();
    }
}
