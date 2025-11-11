namespace HUP.Application.DTOs.AcademicDtos
{
    public class ProgramCreateDto
    {
        public int DepartmentID { get; set; }
        public string ProgramName { get; set; }
        public string DegreeType { get; set; }
        public int DurationYears { get; set; }
        public int Credits { get; set; }
    }
}
