namespace HUP.Application.DTOs.AcademicDtos
{
    public class StudentRegistrationSummaryDto
    {
        public Guid StudentId { get; set; }
        public string CurrentSemester { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public List<RegisteredCourseDto> RegisteredCourses { get; set; }
        public int AvailableCredits { get; set; }
        public int UsedCredits { get; set; }
        public int RemainingCredits { get; set; }
        public bool CanRegister { get; set; }
    }
}
