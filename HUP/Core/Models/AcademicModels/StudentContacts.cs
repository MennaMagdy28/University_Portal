namespace HUP.Core.Models.AcademicModels
{
    public class StudentContacts
    {
        public int StudentID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string AltEmail { get; set; }

        public Student Student { get; set; }
    }
}
