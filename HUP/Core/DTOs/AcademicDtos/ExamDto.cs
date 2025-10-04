namespace HUP.Core.DTOs.AcademicDtos
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string CourseName { get; set; }
        public string ExamType { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public string Location { get; set; }
    }
}
