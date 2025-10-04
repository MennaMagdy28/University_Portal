using HUP.Core.Enums;

namespace HUP.Core.Models.AcademicModels
{
    public class Enrollment
    {
        public int EnrollmentID {  get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public string Semester { get; set; }
        public EnrollmentStatus Status { get; set; }
        public string Grade { get; set; }
        public decimal Points { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
