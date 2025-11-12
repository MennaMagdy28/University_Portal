using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.Academics
{
    public class CourseOffering : BaseEntity
    {
        public Guid CourseID { get; set; }
        public Guid InstructorID { get; set; }
        public Guid SemesterId { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public Semester Semester { get; set; }
    }
}