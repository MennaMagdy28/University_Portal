using HUP.Core.Models.Shared;

namespace HUP.Core.Models.AcademicModels
{
    public class Course : BaseEntity
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
        public int? PrerequisiteID { get; set; }

        public virtual Department Department { get; set; }
        public virtual Course Prerequisite { get; set; }
        public virtual ICollection<Course> PrerequisitesFor { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
