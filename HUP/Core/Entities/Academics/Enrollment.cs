using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentID { get; set; }
        public Guid CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        
        public string Semester { get; set; } // <<<<<=========
        public decimal PercentGrade { get; set; } 
        public EnrollmentStatus Status { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}