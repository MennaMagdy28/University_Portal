using HUP.Core.Models.AcademicModels;

namespace HUP.Core.Models.ServiceModels
{
    public class RolePagePermission
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int PageId { get; set; }
        public Page Page { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
