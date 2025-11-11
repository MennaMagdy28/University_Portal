namespace HUP.Application.DTOs.AcademicDtos
{
    public class StudentCreateDto
    {
        public string NationalID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string UniversityCode { get; set; }
        public int FacultyID { get; set; }
        public int ProgramID { get; set; }
        public int Level { get; set; }
    }
}
