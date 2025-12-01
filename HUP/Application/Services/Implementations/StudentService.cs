using HUP.Application.DTOs.AcademicDtos.Student;
using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<StudentProfileDto> GetStudentProfile(Guid userId)
    {
        var student = await _repository.GetByIdAsync(userId);
        var profile = StudentMapper.ToStudentProfile(student);
        return profile;
    }
}