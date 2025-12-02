using HUP.Core.Entities.Shared;
using HUP.Core.Entities.Identity;
using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Instructor : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public AcademicTitle AcademicTitle { get; set; }

        public User User { get; set; }
        public Department Department { get; set; }
        public Department DepartmentHeaded { get; set; }
        public ICollection<CourseOfferingInstructor> CourseOfferings { get; set; }
    }
}