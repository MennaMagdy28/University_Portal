using HUP.Core.Enums;

namespace HUP.Core.Models.AcademicModels
{
    public class Exam
    {
        public int ExamID { get; set; }
        public int CourseID { get; set; }
        public string ExamName { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public string Location { get; set; }

        public Course Course { get; set; }
    }
}
