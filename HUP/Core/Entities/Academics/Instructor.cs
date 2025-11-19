using HUP.Core.Entities.Shared;
using HUP.Core.Entities.Identity;

namespace HUP.Core.Entities.Academics
{
    public class Instructor : BaseEntity
    {
        public Guid UserID { get; set; }
        public Guid DepartmentID { get; set; }
        public string AcademicTitle { get; set; }

        public User User { get; set; }
        public Department Department { get; set; }
        public ICollection<CourseOfferingInstructor> courseOfferings { get; set; }
    }
}