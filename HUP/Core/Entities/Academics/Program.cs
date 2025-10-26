using HUP.Core.Enums;

namespace HUP.Core.Entities.Academics
{
    public class Program : BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DegreeTYpe DegreeType { get; set; } // ??
        public int DurationInYears { get; set; }
        public int RequiredCredits { get; set; }

        public Department Department { get; set; }
    }
}