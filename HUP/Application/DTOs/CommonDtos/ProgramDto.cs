using HUP.Core.Enums;

namespace HUP.Core.DTOs.CommonDtos
{
    public class ProgramDto
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public DegreeType DegreeType { get; set; }
        public int DurationYears { get; set; }
        public int RequiredCredits { get; set; }

        public DepartmentDto Department { get; set; }
    }
}
