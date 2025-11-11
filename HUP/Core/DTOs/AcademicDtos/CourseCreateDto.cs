namespace HUP.Core.DTOs.AcademicDtos
{
    public class CourseCreateDto
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
        public int? PrerequisiteID { get; set; }
    }
}
