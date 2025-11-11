using HUP.Core.Models.Shared;
using HUP.Core.Models.UserModels;

namespace HUP.Core.Models.ServiceModels
{
    public class RolePagePermission : BaseEntity
    {
        public int RoleID { get; set; }
        public int PageID { get; set; }
        public int PermissionID { get; set; }

        public virtual Role Role { get; set; }
        public virtual Page Page { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
