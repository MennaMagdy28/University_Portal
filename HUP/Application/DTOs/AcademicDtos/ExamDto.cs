using HUP.Core.Enums;

namespace HUP.Application.DTOs.AcademicDtos
{
    public class ExamDto
    {
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public ExamType ExamType { get; set; }
        public string ExamTypeArabic { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string InstructorName { get; set; }
        public bool IsActive { get; set; }
    }
}
