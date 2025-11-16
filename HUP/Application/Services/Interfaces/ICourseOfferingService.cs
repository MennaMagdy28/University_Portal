using HUP.Core.Entities.Academics;

namespace HUP.Application.Services.Interfaces
{
    // Service interface for managing CourseOffering business logic
    // Provides methods for CRUD operations and specific CourseOffering queries
    public interface ICourseOfferingService : IGenericService<CourseOffering>
    {
        // Specific CourseOffering queries
        Task<IEnumerable<CourseOffering>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId);
        Task<IEnumerable<CourseOffering>> GetAvailableToRegisterAsync(Guid studentId);
    }
}

