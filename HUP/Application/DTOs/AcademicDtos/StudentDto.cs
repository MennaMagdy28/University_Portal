using HUP.Core.Enums;

namespace HUP.Core.DTOs.AcademicDtos
{
    public class StudentDto
    {
        public int StudentID { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public string ProfileImage { get; set; }
        public AcademicStatus AcademicStatus { get; set; }
        public int FacultyID { get; set; }
        public int ProgramID { get; set; }
        public int Level { get; set; }
        public decimal CGPA { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
