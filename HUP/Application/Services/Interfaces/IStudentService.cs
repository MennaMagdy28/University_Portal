using HUP.Application.DTOs.AcademicDtos.Student;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Core.Enums;

namespace HUP.Application.Services.Interfaces;

public interface IStudentService
{
    Task<StudentProfileDto> GetStudentProfile(Guid userId);
    Task AddStudent(CreateStudentDto dto);
}
