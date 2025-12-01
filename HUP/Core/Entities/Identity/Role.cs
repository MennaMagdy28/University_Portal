using HUP.Core.Entities.Shared;
using HUP.Core.Entities.Permissions;

namespace HUP.Core.Entities.Identity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string? Description { get; set; }
        public Guid? CreatedBy { get; set; }

        public User CreatedByUser { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
