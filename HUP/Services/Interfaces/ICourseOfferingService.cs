using HUP.Core.Entities.Academics;

namespace HUP.Services.Interfaces
{
    // Service interface for managing CourseOffering business logic
    // Provides methods for CRUD operations and specific CourseOffering queries
    public interface ICourseOfferingService
    {
        // Basic CRUD operations
        Task<CourseOffering> GetByIdAsync(Guid id);
        Task<IEnumerable<CourseOffering>> GetAllAsync();
        Task AddAsync(CourseOffering entity);
        void Update(CourseOffering entity);
        void SoftDelete(Guid id);
        void Remove(Guid id);
        Task SaveChangesAsync();

        // Specific CourseOffering queries
        Task<IEnumerable<CourseOffering>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId);
        Task<IEnumerable<CourseOffering>> GetAvailableToRegisterAsync(Guid studentId);
    }
}

