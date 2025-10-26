namespace HUP.Core.DTOs.AcademicDtos
{
    public class CreateExamDto
    {
        public int CourseId { get; set; }
        public string ExamType { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public string Location { get; set; }
    }
}
