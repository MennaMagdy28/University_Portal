using HUP.Application.DTOs.AcademicDtos.Enrollment;
using HUP.Core.Entities.Academics;

namespace HUP.Application.Services.Interfaces
{
    public interface IEnrollmentService
    {
        //CRUD queries + soft delete
        Task<EnrollmentResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EnrollmentResponseDto>> GetAllAsync();
        Task AddAsync(CreateEnrollmentDto createEnrollmentDto);
        Task UpdateStatus(Guid id, UpdateEnrollmentStatusDto updateEnrollmentDto);
        Task UpdateGrades(Guid id, UpdateEnrollmentGradesDto dto);
        Task SoftDelete(Guid id);
        Task<bool> Exists(CreateEnrollmentDto dto);
        Task Remove(Guid id);
        Task<List<SemesterTranscriptDto>> GetStudentGradesAsync(Guid studentId);
        Task<IEnumerable<EnrollmentResponseDto>> GetRegisteredByStudentAsync(Guid studentId);
    }
}
