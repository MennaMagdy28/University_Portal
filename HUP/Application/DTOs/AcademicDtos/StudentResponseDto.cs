using HUP.Core.Enums;

namespace HUP.Application.DTOs.AcademicDtos
{
    public class StudentResponseDto
    {
        public Guid StudentID { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public string FullName { get; set; }
        public AcademicStatus AcademicStatus { get; set; }
        public string FacultyName { get; set; }
        public string ProgramName { get; set; }
        public int Level { get; set; }
        public decimal CGPA { get; set; }
    }
}
