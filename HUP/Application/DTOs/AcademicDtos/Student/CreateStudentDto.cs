using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Core.Enums;

namespace HUP.Application.DTOs.AcademicDtos.Student
{
    public class CreateStudentDto
    {
        
        public string UniversityCode { get; set; }
        public string UniversityEmail { get; set; }
        public AcademicStatus AcademicStatus { get; set; }
        public Guid DepartmentId { get; set; }
        public int Level { get; set; }
        public decimal Cgpa { get; set; }
        public string Group { get; set; }
        public CreateUserDto UserInfo { get; set; }
    }
}
