using HUP.Core.Enums;

namespace HUP.Core.Models.AcademicModels
{
    public class StudentPersonal
    {
        public int StudentID { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string BirthPlace { get; set; }

        public Student Student { get; set; }
    }
}
