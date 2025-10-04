using HUP.Core.Models.ServiceModels;

namespace HUP.Core.Models.AcademicModels
{
    public class Page
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageDescription { get; set; }

        public ICollection<RolePagePermission> RolePagePermissions { get; set; } = new List<RolePagePermission>();
        public ICollection<UserPagePermission> UserPagePermissions { get; set; } = new List<UserPagePermission>();
    }
}
