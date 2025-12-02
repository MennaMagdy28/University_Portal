using HUP.Core.Enums;
using HUP.Core.Entities.Identity;
using HUP.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;


namespace HUP.Core.Entities.Academics
{
    public class Student
    {
        public Guid UserId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public string? ProfileImage { get; set; } // <<==================
        public AcademicStatus AcademicStatus { get; set; }
        public Guid DepartmentId { get; set; }
        public int Level { get; set; }
        public decimal Cgpa { get; set; } //<<=== general?
        public string Group { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public User User { get; set; }
        public Department Department { get; set; }
    }
}