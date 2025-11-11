using HUP.Core.Enums;
using HUP.Core.Models.Shared;

namespace HUP.Core.Models.AcademicModels
{
    public class Enrollment : BaseEntity
    {
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public string Semester { get; set; }
        public EnrollmentStatus Status { get; set; }
        public string Grade { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
