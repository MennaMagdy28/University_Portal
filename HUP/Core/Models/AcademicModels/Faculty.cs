using HUP.Core.Models.Shared;

namespace HUP.Core.Models.AcademicModels
{
    public class Faculty : BaseEntity
    {
        public string FacultyName { get; set; }
        public string DeanName { get; set; }
        public string ContactInfo { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
