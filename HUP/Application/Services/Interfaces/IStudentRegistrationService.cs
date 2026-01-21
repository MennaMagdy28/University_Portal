using HUP.Application.DTOs.AcademicDtos;

namespace HUP.Application.Services.Interfaces
{
    public interface IStudentRegistrationService
    {
        Task<List<AvailableCourseDto>> GetAvailableCoursesAsync(Guid studentId);
        Task<RegistrationResultDto> RegisterCoursesAsync(RegistrationRequestDto request);
        Task<DropCourseResultDto> DropCourseAsync(Guid studentId, Guid enrollmentId);
        Task<StudentRegistrationSummaryDto> GetRegistrationSummaryAsync(Guid studentId);
        Task<List<ClassGroupDto>> GetStudentScheduleAsync(Guid studentId);
        Task<bool> ValidateRegistrationAsync(Guid studentId, List<Guid> groupIds);
        Task<int> CalculateAllowedCreditsAsync(Guid studentId);
    }
}
