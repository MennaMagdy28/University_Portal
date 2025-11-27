namespace HUP.Application.DTOs.AcademicDtos
{
    public class CourseScheduleCreateDto
    {
        public Guid CourseID { get; set; }
        public Guid InstructorID { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Semester { get; set; }
    }
}
