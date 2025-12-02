using HUP.Core.Enums;

namespace HUP.Core.Entities.Identity
{
    public class UserPersonalInfo
    {
        public Guid UserId { get; set; }
        public string? FullEnglishName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string BirthPlace { get; set; }

    }
}