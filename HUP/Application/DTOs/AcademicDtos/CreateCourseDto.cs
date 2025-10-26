namespace HUP.Core.DTOs.AcademicDtos
{
    public class CreateCourseDto
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
}
