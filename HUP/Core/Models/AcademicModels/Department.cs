namespace HUP.Core.Models.AcademicModels
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public int FacultyID { get; set; }
        public string DepartmentName { get; set; }
        public string HeadOfDepartment { get; set; }

        public Faculty Faculty { get; set; }
    }
}
