using HUP.Core.Enums;

namespace HUP.Core.Models.AcademicModels
{
    public class ProgramEntity
    {
        public int ProgramID { get; set; }
        public int DepartmentID { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public DegreeType DegreeType { get; set; }
        public int DurationYears { get; set; }
        public int RequiredCredits { get; set; }

        public Department Department { get; set; }
    }

}
