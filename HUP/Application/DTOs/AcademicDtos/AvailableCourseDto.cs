namespace HUP.Application.DTOs.AcademicDtos
{
    public class AvailableCourseDto
    {
        public Guid CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int Level { get; set; }
        public bool HasPrerequisite { get; set; }
        public string PrerequisiteCourse { get; set; }
        public bool PrerequisiteMet { get; set; }
        public List<ClassGroupDto> AvailableGroups { get; set; }
        public int MaxAllowedGroups { get; internal set; }
    }
}
