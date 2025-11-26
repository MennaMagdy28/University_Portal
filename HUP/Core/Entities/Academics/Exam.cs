using HUP.Core.Enums;
using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.Academics
{
    public class Exam : BaseEntity
    {
        public Guid CourseOfferingId { get; set; }
        public ExamType ExamType { get; set; }
        public DateOnly ExamDate { get; set; }
        public TimeOnly ExamTime { get; set; }
        public string Location { get; set; }

        public CourseOffering CourseOffering { get; set; }

        public Guid CourseID { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Course Course { get; set; }
    }
}