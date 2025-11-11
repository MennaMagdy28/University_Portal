using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Enums;

namespace HUP.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentResponseDto> GetStudentByIdAsync(int id);
        Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync();
        Task<StudentResponseDto> CreateStudentAsync(StudentCreateDto studentDto);
        Task<StudentResponseDto> UpdateStudentAcademicStatusAsync(int id, AcademicStatus status);
        Task<bool> DeleteStudentAsync(int id);
    }
}
