namespace HUP.Application.DTOs.AcademicDtos
{
    public class ClassGroupDto
    {
        public Guid Id { get; set; }
        public Guid CourseOfferingId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public Guid InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string GroupCode { get; set; }
        public string DayOfWeek { get; set; }
        public string DayOfWeekArabic { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public int Capacity { get; set; }
        public int CurrentEnrollment { get; set; }
        public bool IsFull => CurrentEnrollment >= Capacity;
        public bool IsActive { get; set; }
    }
}
