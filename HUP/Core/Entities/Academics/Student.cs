using HUP.Core.Enums;
using HUP.Core.Entities.Identity;


namespace HUP.Core.Entities.Academics
{
    public class Student
    {
        public Guid UserID { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public string ProfileImage { get; set; } // <<==================
        public AcademicStatus AcademicStatus { get; set; }
        public Guid ProgramID { get; set; }
        public int Level { get; set; }
        public decimal CGPA { get; set; } //<<=== general?

        public User User { get; set; }
        public ProgramEntity Program { get; set; }
    }
}