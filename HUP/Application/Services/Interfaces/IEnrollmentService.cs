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
        Task Update(Guid id, UpdateEnrollmentStatusDto updateEnrollmentDto);
        Task SoftDelete(Guid id);
        Task<bool> Exists(CreateEnrollmentDto dto);
        Task Remove(Guid id);
    }
}
