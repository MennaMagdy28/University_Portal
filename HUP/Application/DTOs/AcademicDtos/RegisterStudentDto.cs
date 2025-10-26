namespace HUP.Core.DTOs.AcademicDtos
{
    public class RegisterStudentDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string NationalID { get; set; }
        public string Phone { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public int FacultyId { get; set; }
        public int ProgramId { get; set; }
    }
}
