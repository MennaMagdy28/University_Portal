using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.Academics
{
    public class Schedule : BaseEntity
    {
        public Guid CourseOfferingId { get; set; }
        public string Group { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public CourseOffering CourseOffering { get; set; }
    }
}