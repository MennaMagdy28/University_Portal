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
}using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using HUP.Core.Enums;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<StudentResponseDto> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return _mapper.Map<StudentResponseDto>(student);
        }

        public async Task<StudentResponseDto> UpdateStudentAcademicStatusAsync(Guid id, AcademicStatus status)
        {
            await _studentRepository.UpdateAcademicStatusAsync(id, status);
            return await GetStudentByIdAsync(id);
        }

        public async Task<bool> UploadProfileImageAsync(Guid studentId, string imagePath)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return false;

            student.ProfileImage = imagePath;
            student.UpdatedAt = DateTime.UtcNow;

            await _studentRepository.UpdateAsync(student);
            return true;
        }
    }
}
