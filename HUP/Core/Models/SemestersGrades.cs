namespace HUP.Core.Models
{
    public class SemesterGrades
    {
        public Guid SemesterId { get; set; }
        public string SemesterName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public decimal TotalGrade { get; set; }
    }
}