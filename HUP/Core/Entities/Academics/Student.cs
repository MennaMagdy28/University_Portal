using HUP.Core.Enums;
using HUP.Core.Entities.Identity;
using HUP.Core.Entities.Shared;


namespace HUP.Core.Entities.Academics
{
    public class Student : BaseEntity
    {
        public Guid UserId => Id;
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public string? ProfileImage { get; set; } // <<==================
        public AcademicStatus AcademicStatus { get; set; }
        public Guid DepartmentId { get; set; }
        public int Level { get; set; }
        public decimal Cgpa { get; set; } //<<=== general?
        public string Group { get; set; }

        public User User { get; set; }
        public Department Department { get; set; }

        public Guid FacultyID { get; set; }
        public Guid ProgramID { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual ProgramPlan Program { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}