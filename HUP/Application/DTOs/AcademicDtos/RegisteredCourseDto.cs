namespace HUP.Application.DTOs.AcademicDtos
{
    public class RegisteredCourseDto
    {
        public Guid EnrollmentId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public string GroupCode { get; set; }
        public string DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string InstructorName { get; set; }
        public string EnrollmentStatus { get; set; }
        public bool CanDrop { get; set; }
    }
}
