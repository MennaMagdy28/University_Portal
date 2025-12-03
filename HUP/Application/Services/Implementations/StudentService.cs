using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.DTOs.AcademicDtos.Student;
using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Core.Entities.Identity;
using HUP.Core.Enums;
using HUP.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HUP.Application.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _hasher;

    public StudentService(IStudentRepository studentRepository, IUserRepository userRepository, IPasswordHasher<User> hasher, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _hasher = hasher;
        _mapper = mapper;
    }
    
    public async Task<StudentProfileDto> GetStudentProfile(Guid userId)
    {
        var student = await _studentRepository.GetByIdAsync(userId);
        if (student == null)
            return null;
        var profile = StudentMapper.ToStudentProfile(student);
        return profile;
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
        student.User.UpdatedAt = DateTime.UtcNow;

        await _studentRepository.UpdateAsync(student);
        return true;
    }

    public async Task AddStudent(CreateStudentDto dto)
    {
        var student = StudentMapper.ToCreateStudent(dto);
        var user = student.User;
        user.CreatedAt = DateTime.UtcNow;
        var hashed = _hasher.HashPassword(user, dto.UserInfo.PasswordHash);
        user.PasswordHash = hashed;
        user.Id = new Guid();
        await _userRepository.AddAsync(user);
        student.UserId = user.Id;
        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();
    }
}
