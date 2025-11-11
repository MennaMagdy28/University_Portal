using HUP.Core.Enums;
using HUP.Core.Entities.Shared;
using HUP.Core.Entities.UserModels;

namespace HUP.Core.Entities.AcademicModels
{
    public class Student : BaseEntity
    {
        public int UserID { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public string? ProfileImage { get; set; }
        public AcademicStatus AcademicStatus { get; set; }
        public int FacultyID { get; set; }
        public int ProgramID { get; set; }
        public int Level { get; set; }
        public decimal CGPA { get; set; }

        public virtual User User { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual ProgramEntity Program { get; set; }
        public virtual StudentPersonal StudentPersonal { get; set; }
        public virtual StudentContacts StudentContacts { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
