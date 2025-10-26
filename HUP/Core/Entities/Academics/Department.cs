namespace HUP.Core.Entities.Academics
{
    public class Department : BaseEntity
    {
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public Guid FacultyID { get; set; }
        public string HeadOfDepartment { get; set; }

        public Faculty Faculty { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
}