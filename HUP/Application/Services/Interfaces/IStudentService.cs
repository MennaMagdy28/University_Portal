using HUP.Application.DTOs.AcademicDtos.Student;

namespace HUP.Application.Services.Interfaces;

public interface IStudentService
{
    Task<StudentProfileDto> GetStudentProfile(Guid userId);
}