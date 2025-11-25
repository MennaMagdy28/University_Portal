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
        Task Update(Guid id, UpdateEnrollmentDto updateEnrollmentDto);
        Task SoftDelete(Guid id);
        Task Remove(Guid id);
    }
}
