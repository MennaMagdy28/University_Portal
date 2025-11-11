using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using HUP.Core.Enums;
using HUP.Core.Models.AcademicModels;
using HUP.Core.Models.UserModels;
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

        public async Task<StudentResponseDto> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return _mapper.Map<StudentResponseDto>(student);
        }

        public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
        }

        public async Task<StudentResponseDto> CreateStudentAsync(StudentCreateDto studentDto)
        {
            // Check if user already exists
            if (await _userRepository.UserExistsAsync(studentDto.NationalID, studentDto.Email))
            {
                throw new ArgumentException("Student with this National ID or Email already exists.");
            }

            // Create user first
            var user = _mapper.Map<User>(studentDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(studentDto.NationalID); // Default password is National ID
            user.Role = RoleType.Student;

            await _userRepository.AddAsync(user);

            // Create student record
            var student = new Student
            {
                UserID = user.Id,
                UniversityCode = studentDto.UniversityCode,
                UniversityEmail = $"{studentDto.UniversityCode}@university.edu",
                FacultyID = studentDto.FacultyID,
                ProgramID = studentDto.ProgramID,
                Level = studentDto.Level,
                AcademicStatus = AcademicStatus.Active,
                CGPA = 0.0m
            };

            await _studentRepository.AddAsync(student);
            return await GetStudentByIdAsync(student.Id);
        }

        public async Task<StudentResponseDto> UpdateStudentAcademicStatusAsync(int id, AcademicStatus status)
        {
            await _studentRepository.UpdateAcademicStatusAsync(id, status);
            return await GetStudentByIdAsync(id);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student != null)
            {
                await _userRepository.DeleteAsync(student.UserID);
                return true;
            }
            return false;
        }
    }
}
