using HUP.Core.Enums;

namespace HUP.Core.Entities.Identity
{
    public class UserPersonal : BaseEntity
    {
        public Guid StudentID { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string BirthPlace { get; set; }

        public Student Student { get; set; }
    }
}