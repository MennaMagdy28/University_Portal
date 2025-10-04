namespace HUP.Core.Models.AcademicModels
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
        public int PrerequisiteID { get; set; }

        public Department Department { get; set; }
        public Course Prerequisite { get; set; }
    }
}
