namespace HUP.Application.DTOs.AcademicDtos
{
    public class StudentTimetableDto
    {
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string InstructorName { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
    }
}
