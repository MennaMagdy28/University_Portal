namespace HUP.Core.DTOs.AcademicDtos
{
    public class CreateCourseOfferingDto
    {
        public Guid CourseId { get; set; }
        public Guid SemesterId { get; set; }
        public Guid DepartmentId { get; set; }
    }
}

