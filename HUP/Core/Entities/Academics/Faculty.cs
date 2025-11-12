using HUP.Core.Entities.Shared;
using HUP.Core.Entities.Identity;

namespace HUP.Core.Entities.Academics
{
    public class Faculty : BaseEntity
    {
        public string FacultyName { get; set; }
        public Guid DeanID { get; set; } // <<======
        public string DeanName { get; set; } // xx?
        public string ContactInfo { get; set; }

        public User Dean { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}