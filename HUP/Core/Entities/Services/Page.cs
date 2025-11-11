using HUP.Core.Entities.Shared;
using HUP.Core.Entities.UserModels;

namespace HUP.Core.Entities.ServiceModels
{
    public class Page : BaseEntity
    {
        public string PageName { get; set; }
        public string PageDescription { get; set; }

        public virtual ICollection<RolePagePermission> RolePagePermissions { get; set; }
        public virtual ICollection<UserPagePermission> UserPagePermissions { get; set; }
    }
}
