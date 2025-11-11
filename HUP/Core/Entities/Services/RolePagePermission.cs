using HUP.Core.Entities.Shared;
using HUP.Core.Entities.UserModels;

namespace HUP.Core.Entities.ServiceModels
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
