using HUP.Core.Models.Shared;
using HUP.Core.Models.UserModels;

namespace HUP.Core.Models.AcademicModels
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
