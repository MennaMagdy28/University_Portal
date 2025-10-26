using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Exam : BaseEntity
    {
        public Guid CourseID { get; set; }
        public Guid InstructorID { get; set; }
        public ExamType ExamType { get; set; } //<<=======
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public string Location { get; set; }

        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}