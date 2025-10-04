using HUP.Core.Models.AcademicModels;
using HUP.Core.Models.UserModels;

namespace HUP.Core.Models.ServiceModels
{
    public class UserPagePermission
    {
        public int UserPagePermissionId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int PageId { get; set; }
        public Page Page { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
