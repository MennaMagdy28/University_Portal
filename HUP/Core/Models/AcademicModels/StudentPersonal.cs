using HUP.Core.Enums;
using HUP.Core.Models.Shared;

namespace HUP.Core.Models.AcademicModels
{
    public class StudentPersonal : BaseEntity
    {
        public int StudentID { get; set; }
        public Gender Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string BirthPlace { get; set; }

        public virtual Student Student { get; set; }
    }
}
