using HUP.Core.Entities.Shared;
using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid SemesterId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        
        public decimal? ClassGrade { get; set; }
        public decimal? MidtermGrade { get; set; }
        public decimal? finalGrade {get; set; }
        public EnrollmentStatus Status { get; set; }

        public virtual Semester Semester { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }

        public string SemesterName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}