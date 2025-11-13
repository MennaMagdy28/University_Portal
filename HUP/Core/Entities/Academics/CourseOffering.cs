using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.Academics
{
    public class CourseOffering : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Guid InstructorId { get; set; }
        public Guid SemesterId { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public Semester Semester { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}