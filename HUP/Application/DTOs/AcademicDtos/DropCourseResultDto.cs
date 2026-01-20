namespace HUP.Application.DTOs.AcademicDtos
{
    public class DropCourseResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid? DroppedEnrollmentId { get; set; }
    }
}
