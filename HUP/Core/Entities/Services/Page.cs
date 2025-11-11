using HUP.Core.Models.Shared;
using HUP.Core.Models.UserModels;

namespace HUP.Core.Models.ServiceModels
{
    public class Page : BaseEntity
    {
        public string PageName { get; set; }
        public string PageDescription { get; set; }

        public virtual ICollection<RolePagePermission> RolePagePermissions { get; set; }
        public virtual ICollection<UserPagePermission> UserPagePermissions { get; set; }
    }
}
