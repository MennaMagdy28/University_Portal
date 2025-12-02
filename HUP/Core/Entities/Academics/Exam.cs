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

        public virtual CourseOffering CourseOffering { get; set; }
    }
}