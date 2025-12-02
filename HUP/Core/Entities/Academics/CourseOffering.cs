using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.Academics
{
    // To offer a course to be available for students to enroll in a given semester
    // Used to create class schedules, exams and manage enrollments
    public class CourseOffering : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Guid SemesterId { get; set; }
        public Guid DepartmentId { get; set; }

        public Department Department { get; set; }
        public Course Course { get; set; }
        public Semester Semester { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseOfferingInstructor> Instructors { get; set; }
    }
}