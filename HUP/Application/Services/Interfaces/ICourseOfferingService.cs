using HUP.Application.DTOs.AcademicDtos.CourseOffering;
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
        Task AddAsync(CreateCourseOfferingDto courseOfferingDto);
        // Task Update(Guid id, CreateCourseOfferingDto courseOfferingDto);
        Task SoftDelete(Guid id);
        Task Remove(Guid id);

        Task<bool> Exists(CreateCourseOfferingDto dto);
        // Specific CourseOffering queries
        Task<IEnumerable<CourseOfferingDto>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId);
        Task<IEnumerable<CourseOfferingDto>> GetAvailableToRegisterAsync(Guid studentId);
    }
}

