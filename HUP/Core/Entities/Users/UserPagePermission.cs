using HUP.Core.Entities.ServiceModels;
using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.UserModels
{
    public class UserPagePermission : BaseEntity
    {
        public int UserID { get; set; }
        public int PageID { get; set; }
        public int PermissionID { get; set; }

        public virtual User User { get; set; }
        public virtual Page Page { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
