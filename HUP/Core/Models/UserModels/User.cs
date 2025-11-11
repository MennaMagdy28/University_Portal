using HUP.Core.Enums;
using HUP.Core.Models.AcademicModels;
using HUP.Core.Models.Shared;

namespace HUP.Core.Models.UserModels
{
    public class User : BaseEntity
    {
        public string NationalID { get; set; }
        public string UniversityEmail { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public RoleType Role { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserPagePermission> UserPagePermissions { get; set; }
        public virtual Student Student { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
