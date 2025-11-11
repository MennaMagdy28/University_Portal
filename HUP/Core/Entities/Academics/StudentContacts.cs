using HUP.Core.Models.Shared;

namespace HUP.Core.Models.AcademicModels
{
    public class StudentContacts : BaseEntity
    {
        public int StudentID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string AltEmail { get; set; }

        public virtual Student Student { get; set; }
    }
}
