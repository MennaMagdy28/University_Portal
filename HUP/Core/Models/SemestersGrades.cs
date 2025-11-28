namespace HUP.Core.Models
{
    public class SemesterGrades
    {
        public Guid SemesterId { get; set; }
        public string SemesterName { get; set; }
        public Guid CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public decimal ClassGrade { get; set; }
        public decimal MidtermGrade { get; set; }
        public decimal FinalGrade { get; set; }
        public int CourseCredits { get; set; }
    }
}