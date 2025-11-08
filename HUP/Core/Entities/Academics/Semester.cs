namespace HUP.Core.Entities.Academics
{
    public class Semester : BaseEntity
    {
        public string SemesterName { get; set; } // e.g., "Fall 2024"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}