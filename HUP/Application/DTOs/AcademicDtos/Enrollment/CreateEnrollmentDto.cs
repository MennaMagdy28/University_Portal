using HUP.Core.Enums;

namespace HUP.Application.DTOs.AcademicDtos.Enrollment
{
    public class CreateEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseOfferingId { get; set; }
    }
}
