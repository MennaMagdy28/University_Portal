using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.Academics
{
    public class ClassGroup : BaseEntity
    {
        public Guid CourseOfferingId { get; set; }
        public Guid InstructorId { get; set; }
        public string GroupCode { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public int Capacity { get; set; }
        public int CurrentEnrollment { get; set; }
        public bool IsActive { get; set; } = true;

        public CourseOffering CourseOffering { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
