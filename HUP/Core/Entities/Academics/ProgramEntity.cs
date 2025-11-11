using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.AcademicModels
{
    public class ProgramEntity : BaseEntity
    {
        public int DepartmentID { get; set; }
        public string ProgramName { get; set; }
        public string DegreeType { get; set; }
        public int DurationYears { get; set; }
        public int Credits { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
