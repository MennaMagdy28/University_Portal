using HUP.Core.Entities.Shared;
using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        
        public decimal? PercentGrade { get; set; } 
        public EnrollmentStatus Status { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }

        public string Semester { get; set; }
        public bool IsActive { get; set; } = true;
    }
}