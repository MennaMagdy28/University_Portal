using HUP.Core.Enums;
using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.AcademicModels
{
    public class Exam : BaseEntity
    {
        public int CourseID { get; set; }
        public ExamType ExamType { get; set; }
        public DateOnly ExamDate { get; set; }
        public TimeOnly ExamTime { get; set; }
        public string Location { get; set; }

        public virtual Course Course { get; set; }
    }
}
