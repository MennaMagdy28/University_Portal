namespace HUP.Application.DTOs.AcademicDtos
{
    public class CreateClassGroupDto
    {
        public Guid CourseOfferingId { get; set; }
        public Guid InstructorId { get; set; }
        public string GroupCode { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public int Capacity { get; set; }
    }
}
