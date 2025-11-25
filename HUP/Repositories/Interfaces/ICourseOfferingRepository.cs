using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    // Repository interface for managing CourseOffering entities
    // Extends the generic repository interface for basic CRUD operations
    // Defines additional queries specific to CourseOffering
    public interface ICourseOfferingRepository : IGenericRepository<CourseOffering>
    {
        //All course offerings for a specific department and semester
        Task<IEnumerable<CourseOffering>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId);
        // Retrieves course offerings that are available for the student to register in a specific department and semester
        Task<IEnumerable<CourseOffering>> GetAvailableToRegisterAsync(Guid studentId);
        Task<CourseOffering?> GetExistingAsync(Guid courseId, Guid deptId, Guid semesterId);

    }
}
