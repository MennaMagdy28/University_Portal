using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;

namespace HUP.Application.Services.Interfaces
{
    public interface IEnrollmentService
    {
        //CRUD queries + soft delete
        Task<CreateEnrollmentDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<CreateEnrollmentDto>> GetAllAsync();
        Task AddAsync(CreateEnrollmentDto createEnrollmentDto);
        void Update(CreateEnrollmentDto createEnrollmentDto);
        void SoftDelete(Guid id);
        void Remove(Guid id);
        Task SaveChangesAsync();
    }
}
