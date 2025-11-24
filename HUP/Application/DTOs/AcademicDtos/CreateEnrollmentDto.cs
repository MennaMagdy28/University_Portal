namespace HUP.Application.DTOs.AcademicDtos
{
    public class CreateEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
