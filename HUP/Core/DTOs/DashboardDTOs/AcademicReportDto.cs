namespace HUP.Core.DTOs.DashboardDTOs
{
    public class AcademicReportDto
    {
        public string ProgramName { get; set; }
        public int TotalStudents { get; set; }
        public decimal AverageCGPA { get; set; }
        public int GraduatedCount { get; set; }
    }
}
