using HUP.Core.Entities.Shared;
using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid CourseOfferingId { get; set; }
        public Guid ClassGroupId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        
        public decimal ClassGrade { get; set; }
        public decimal MidtermGrade { get; set; }
        public decimal FinalGrade {get; set; }
        public EnrollmentStatus Status { get; set; }

        public Student Student { get; set; }
        public CourseOffering CourseOffering { get; set; }
        public ClassGroup ClassGroup { get; set; }
    }
}