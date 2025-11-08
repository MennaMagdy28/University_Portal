using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Exam : BaseEntity
    {
        public Guid CousreOfferingId { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public string Location { get; set; }

        public CourseOffering CourseOffering { get; set; }
    }
}