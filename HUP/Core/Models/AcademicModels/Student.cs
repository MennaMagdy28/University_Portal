using HUP.Core.Enums;
using HUP.Core.Models.UserModels;


namespace HUP.Core.Models.AcademicModels
{
    public class Student
    {
        public int StudentID { get; set; }
        public int UserID { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public string ProfileImage { get; set; }
        public AcademicStatus AcademicStatus { get; set; }
        public int FacultyID { get; set; }
        public int ProgramID { get; set; }
        public int Level { get; set; }
        public decimal CGPA { get; set; }

        public User User { get; set; }
    }
}
