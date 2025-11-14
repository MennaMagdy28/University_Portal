using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.Academics
{
    public class Department : BaseEntity
    {
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public Guid FacultyID { get; set; }
        public string HeadOfDepartment { get; set; }
        public int DurationInYears { get; set; }
        public int CompulsoryHours { get; set; }
        public int ElectiveHours { get; set; }

        public Faculty Faculty { get; set; }
        public ICollection<ProgramPlan> ProgramPlan { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
        public ICollection<ProgramPlan> Programs { get; set; }
    }
}