using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.AcademicModels
{
    public class Department : BaseEntity
    {
        public int FacultyID { get; set; }
        public string DepartmentName { get; set; }
        public string HeadOfDepartment { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<ProgramEntity> Programs { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
