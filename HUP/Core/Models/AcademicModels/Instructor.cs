using HUP.Core.Models.UserModels;

namespace HUP.Core.Models.AcademicModels
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public string AcademicTitle { get; set; }

        public User User { get; set; }
        public Department Department { get; set; }
    }
}
