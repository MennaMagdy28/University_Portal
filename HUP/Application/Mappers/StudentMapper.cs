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

    public static Student ToCreateStudent(CreateStudentDto dto)
    {
        var student = new Student();
        student = MapEntityFields(dto);
        student.User = UserMapper.ToCreateEntity(dto.UserInfo);
        return student;
    }
    public static partial Student MapEntityFields(CreateStudentDto dto); 

}