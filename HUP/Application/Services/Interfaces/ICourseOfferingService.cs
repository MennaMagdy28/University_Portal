using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;

namespace HUP.Application.Services.Interfaces
{
    // Service interface for managing CourseOffering business logic
    // Provides methods for CRUD operations and specific CourseOffering queries
    public interface ICourseOfferingService
    {
        // CRUD queries + soft delete
        Task<CourseOfferingDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<CourseOfferingDto>> GetAllAsync();
        Task AddAsync(CourseOfferingDto courseOfferingDto);
        void Update(CourseOfferingDto courseOfferingDto);
        void SoftDelete(Guid id);
        void Remove(Guid id);
        Task SaveChangesAsync();

        // Specific CourseOffering queries
        Task<IEnumerable<CourseOfferingDto>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId);
        Task<IEnumerable<CourseOfferingDto>> GetAvailableToRegisterAsync(Guid studentId);
    }
}

