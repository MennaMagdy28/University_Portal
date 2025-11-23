using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;

namespace HUP.Application.Services.Interfaces
{
    public interface IEnrollmentService
    {
        //CRUD queries + soft delete
        Task<EnrollmentResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EnrollmentResponseDto>> GetAllAsync();
        Task AddAsync(CreateEnrollmentDto createEnrollmentDto);
        Task Update(EnrollmentResponseDto createEnrollmentDto);
        Task SoftDelete(Guid id);
        Task Remove(Guid id);
    }
}
