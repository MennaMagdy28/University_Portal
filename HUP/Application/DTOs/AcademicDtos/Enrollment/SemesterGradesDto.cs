namespace HUP.Application.DTOs.AcademicDtos.Enrollment
{
    public class SemesterTranscriptDto
    {
        public string SemesterName { get; set; }
        public List<SemesterGradesDto> Courses { get; set; }
        public decimal SemesterGPA { get; set; }
        public decimal CumulativeGPA { get; set; }
    }
    
    public class SemesterGradesDto
    {
        public Guid SemesterId { get; set; }
        public string SemesterName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public decimal TotalGrade { get; set; }
        public string Grade { get; set; }
        public int CreditHours { get; set; }
        public decimal GradePoints { get; set; }
        public decimal CreditPoints { get; set; }
    }
}