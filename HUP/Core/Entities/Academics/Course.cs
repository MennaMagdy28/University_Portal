
namespace HUP.Core.Entities.Academics
{
    public class Course : BaseEntity
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid PrerequisiteID { get; set; }

        public Department Department { get; set; }
        public Course Prerequisite { get; set; }
    }
}