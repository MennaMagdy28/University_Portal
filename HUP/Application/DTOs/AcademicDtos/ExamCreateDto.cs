using HUP.Core.Enums;

namespace HUP.Application.DTOs.AcademicDtos
{
    public class ExamCreateDto
    {
        public Guid CourseID { get; set; }
        public ExamType ExamType { get; set; }
        public DateOnly ExamDate { get; set; }
        public TimeOnly ExamTime { get; set; }
        public string Location { get; set; }
    }
}
