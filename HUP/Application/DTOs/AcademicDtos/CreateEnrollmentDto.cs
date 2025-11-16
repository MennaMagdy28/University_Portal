using HUP.Core.Enums;

namespace HUP.Core.DTOs.AcademicDtos
{
    public class CreateEnrollmentDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
