using HUP.Core.Entities.Shared;
using HUP.Core.Entities.Identity;
using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Faculty : BaseEntity
    {
        public FacultyTitle Name { get; set; }
        public string DisplayName { get; set; }
        public Guid DeanId { get; set; } // <<======
        public string DeanName { get; set; } // xx?
        public string ContactInfo { get; set; }

        public User Dean { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}