namespace HUP.Application.DTOs.AcademicDtos
{
    public class CourseScheduleDto
    {
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public Guid InstructorID { get; set; }
        public string InstructorName { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string DayOfWeekArabic { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Semester { get; set; }
        public bool IsActive { get; set; }
    }
}
