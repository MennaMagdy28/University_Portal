using HUP.Core.Enums;

namespace HUP.Application.DTOs.AcademicDtos.Enrollment
{
    public class UpdateEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
        public decimal grade { get; set; }
    }
}