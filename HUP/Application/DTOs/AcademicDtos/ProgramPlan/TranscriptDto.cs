using HUP.Core.Enums;

namespace HUP.Application.DTOs.AcademicDtos.ProgramPlan
{
    public class TranscriptDto
    {
        public List<ProgramPlanItemDto> UniversityCompulsory { get; set; }
        public List<ProgramPlanItemDto> UniversityElective { get; set; }

        public List<ProgramPlanItemDto> FacultyCompulsory { get; set; }
        public List<ProgramPlanItemDto> FacultyElective { get; set; }
        
        public List<ProgramPlanItemDto> DepartmentCompulsory { get; set; }
        public List<ProgramPlanItemDto> DepartmentElective { get; set; }
    }

    public class ProgramPlanItemDto
    {
        public string Code { get; set; }
        public string CourseName { get; set; }
        public int Hours { get; set; }
        public decimal Total { get; set; }
        public string Grade { get; set; }

        public bool IsCompulsory { get; set; }
        public RequirementType RequirementType { get; set; }
    }

}
