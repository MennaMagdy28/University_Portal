using HUP.Application.DTOs.AcademicDtos.Student;
using HUP.Core.Entities.Academics;
using Riok.Mapperly.Abstractions;

namespace HUP.Application.Mappers;
[Mapper]
public static partial class StudentMapper
{
    public static StudentProfileDto ToStudentProfile(Student student)
    {
        var profile = MapStudentFields(student);
        profile.ProfileInfo = UserMapper.ToProfileDto(student.User);
        return profile;
    }
    public static partial StudentProfileDto MapStudentFields(Student student);
}