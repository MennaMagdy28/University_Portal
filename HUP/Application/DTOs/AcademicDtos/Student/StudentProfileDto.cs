using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Core.Enums;

namespace HUP.Application.DTOs.AcademicDtos.Student;

public class StudentProfileDto
{
    public string UniversityCode { get; set; }
    public string? ProfileImage { get; set; }
    public AcademicStatus AcademicStatus { get; set; }
    public int Level { get; set; }
    public decimal Cgpa { get; set; }
    public string Group { get; set; }
    public ProfileInfoDto ProfileInfo { get; set; }
}