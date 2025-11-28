namespace HUP.Application.DTOs.AcademicDtos
{
    public class StudentExamScheduleDto
    {
        public Guid ExamId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string ExamType { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string InstructorName { get; set; }
        public string ExamDateFormatted => ExamDate.ToString("yyyy/MM/dd");
        public string ExamTimeFormatted => ExamTime.ToString(@"hh\:mm");
        public string DayOfWeek { get; set; }
    }
}
