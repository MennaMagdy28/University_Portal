using HUP.Application.DTOs.AcademicDtos;
using HUP.Core.Enums;

namespace HUP.Application.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentResponseDto> GetStudentByIdAsync(Guid id);
        Task<StudentResponseDto> UpdateStudentAcademicStatusAsync(Guid id, AcademicStatus status);
        Task<bool> UploadProfileImageAsync(Guid studentId, string imagePath);
    }
}
