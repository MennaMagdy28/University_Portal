using HUP.Core.Entities.Shared;
using HUP.Core.Entities.UserModels;

namespace HUP.Core.Entities.AcademicModels
{
    public class Instructor : BaseEntity
    {
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public string AcademicTitle { get; set; }

        public virtual User User { get; set; }
        public virtual Department Department { get; set; }
    }
}
